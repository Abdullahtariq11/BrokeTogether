using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Application.Service.Contracts.Models;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class ContributionSplitService : IContributionSplitService
    {
        private readonly IRepositoryManager _uow;
        private readonly ILogger<ContributionSplitService> _logger;

        public ContributionSplitService(IRepositoryManager repositoryManager, ILogger<ContributionSplitService> logger)
        {
            _uow = repositoryManager;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<ContributionSplit>> AddSplitsAsync(
            Guid contributionId,
            IEnumerable<SplitInput> splits)
        {
            if (splits is null) throw new ArgumentNullException(nameof(splits));

            var requested = splits.ToList();
            if (requested.Count == 0)
                return Array.Empty<ContributionSplit>();

            // Basic per-item input validation
            foreach (var s in requested)
            {
                if (string.IsNullOrWhiteSpace(s.UserId))
                    throw new ArgumentException("Split must include a valid UserId.", nameof(splits));
                if (s.Amount < 0m)
                    throw new ArgumentException("Split amount cannot be negative.", nameof(splits));
            }

            // Enforce uniqueness within the incoming request
            var dupUser = requested.GroupBy(s => s.UserId, StringComparer.Ordinal)
                                   .FirstOrDefault(g => g.Count() > 1);
            if (dupUser != null)
                throw new InvalidOperationException($"Duplicate split for user '{dupUser.Key}' in request.");

            // Load contribution
            var contribution = await _uow.ContributionRepository.GetByIdAsync(contributionId, trackChanges: false);
            if (contribution is null)
                throw new InvalidOperationException($"Contribution '{contributionId}' was not found.");

            // Validate membership for all users in one go
            var memberSet = (await _uow.HomeMemberRepository
                    .GetMembersByHomeIdAsync(contribution.HomeId, trackChanges: false))
                .Select(m => m.UserId)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var s in requested)
            {
                if (!memberSet.Contains(s.UserId))
                    throw new InvalidOperationException($"User '{s.UserId}' is not a member of home '{contribution.HomeId}'.");
            }

            // Load existing splits for this contribution
            var existingForContribution = (await _uow.ContributionSplitRepository
                    .GetByContributionIdAsync(contributionId, trackChanges: false))
                .ToList();

            var existingUserIds = existingForContribution
                .Select(cs => cs.UserId)
                .ToHashSet(StringComparer.Ordinal);

            // Prepare only new splits (non-overwrite behavior)
            var toAdd = new List<ContributionSplit>(requested.Count);
            foreach (var s in requested)
            {
                if (!existingUserIds.Contains(s.UserId))
                {
                    toAdd.Add(new ContributionSplit
                    {
                        ContributionId = contributionId,
                        UserId = s.UserId,
                        AmountOwed = s.Amount
                    });
                }
            }

            // Strict total policy with tolerance
            const decimal epsilon = 0.01m;
            var totalAfter = existingForContribution.Sum(x => x.AmountOwed) + toAdd.Sum(x => x.AmountOwed);
            if (Math.Abs(totalAfter - contribution.TotalAmount) > epsilon)
            {
                throw new InvalidOperationException(
                    $"Split total {totalAfter:F2} does not match contribution total {contribution.TotalAmount:F2}.");
            }

            if (toAdd.Count > 0)
            {
                await _uow.ContributionSplitRepository.AddRangeAsync(toAdd);
                await _uow.SaveAsync();
            }

            return toAdd.AsReadOnly();
        }

        public async Task<IEnumerable<ContributionSplit>> GetByContributionIdAsync(Guid contributionId)
        {
            return await _uow.ContributionSplitRepository.GetByContributionIdAsync(contributionId, trackChanges: false);
        }

        public async Task<ContributionSplit?> GetByIdsAsync(Guid contributionId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.", nameof(userId));

            return await _uow.ContributionSplitRepository.GetByIdsAsync(contributionId, userId, trackChanges: false);
        }

        public async Task<IEnumerable<ContributionSplit>> GetByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.", nameof(userId));

            return await _uow.ContributionSplitRepository.GetByUserIdAsync(userId, trackChanges: false);
        }

        public async Task<ContributionSplit?> UpdateSplitAsync(Guid contributionId, string userId, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.", nameof(userId));
            if (amount < 0m)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));

            // Load entities needed
            var split = await _uow.ContributionSplitRepository.GetByIdsAsync(contributionId, userId, trackChanges: true);
            if (split is null)
                throw new InvalidOperationException($"Split for contribution '{contributionId}' and user '{userId}' not found.");

            var contribution = await _uow.ContributionRepository.GetByIdAsync(contributionId, trackChanges: false);
            if (contribution is null)
                throw new InvalidOperationException($"Contribution '{contributionId}' was not found.");

            // Compute total with new amount (without saving yet)
            const decimal epsilon = 0.01m;
            var otherSplits = await _uow.ContributionSplitRepository.GetByContributionIdAsync(contributionId, trackChanges: false);
            var totalOther = otherSplits.Where(s => !string.Equals(s.UserId, userId, StringComparison.Ordinal))
                                        .Sum(s => s.AmountOwed);

            var totalAfter = totalOther + amount;
            if (Math.Abs(totalAfter - contribution.TotalAmount) > epsilon)
            {
                throw new InvalidOperationException(
                    $"Split total {totalAfter:F2} does not match contribution total {contribution.TotalAmount:F2}.");
            }

            // Apply and persist
            split.AmountOwed = amount;
            await _uow.ContributionSplitRepository.UpdateAsync(split);
            await _uow.SaveAsync();

            return split;
        }

        public async Task RemoveSplitAsync(Guid contributionId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("UserId is required.", nameof(userId));

            var split = await _uow.ContributionSplitRepository.GetByIdsAsync(contributionId, userId, trackChanges: true);
            if (split is null)
                return; // idempotent remove

            await _uow.ContributionSplitRepository.DeleteAsync(split);
            await _uow.SaveAsync();
        }

        public async Task<IReadOnlyCollection<ContributionSplit>> SetSplitsForContributionAsync(
            Guid contributionId,
            IEnumerable<SplitInput> splits)
        {
            if (splits is null) throw new ArgumentNullException(nameof(splits));

            var requested = splits.ToList();

            // Load contribution
            var contribution = await _uow.ContributionRepository.GetByIdAsync(contributionId, trackChanges: false);
            if (contribution is null)
                throw new InvalidOperationException($"Contribution '{contributionId}' was not found.");

            // Validate inputs
            if (requested.Count == 0)
                throw new ArgumentException("At least one split is required.", nameof(splits));

            foreach (var s in requested)
            {
                if (string.IsNullOrWhiteSpace(s.UserId))
                    throw new ArgumentException("Split must include a valid UserId.", nameof(splits));
                if (s.Amount < 0m)
                    throw new ArgumentException("Split amount cannot be negative.", nameof(splits));
            }

            var dupUser = requested.GroupBy(s => s.UserId, StringComparer.Ordinal)
                                   .FirstOrDefault(g => g.Count() > 1);
            if (dupUser != null)
                throw new InvalidOperationException($"Duplicate split for user '{dupUser.Key}' in request.");

            // Validate membership once
            var memberSet = (await _uow.HomeMemberRepository
                    .GetMembersByHomeIdAsync(contribution.HomeId, trackChanges: false))
                .Select(m => m.UserId)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var s in requested)
            {
                if (!memberSet.Contains(s.UserId))
                    throw new InvalidOperationException($"User '{s.UserId}' is not a member of home '{contribution.HomeId}'.");
            }

            // Strict total policy with tolerance
            const decimal epsilon = 0.01m;
            var totalRequested = requested.Sum(x => x.Amount);
            if (Math.Abs(totalRequested - contribution.TotalAmount) > epsilon)
            {
                throw new InvalidOperationException(
                    $"Split total {totalRequested:F2} does not match contribution total {contribution.TotalAmount:F2}.");
            }

            // Replace strategy: remove all existing splits, add requested
            var existing = (await _uow.ContributionSplitRepository
                .GetByContributionIdAsync(contributionId, trackChanges: true)).ToList();

            foreach (var cs in existing)
                await _uow.ContributionSplitRepository.DeleteAsync(cs);

            var toAdd = requested.Select(s => new ContributionSplit
            {
                ContributionId = contributionId,
                UserId = s.UserId,
                AmountOwed = s.Amount
            }).ToList();

            if (toAdd.Count > 0)
                await _uow.ContributionSplitRepository.AddRangeAsync(toAdd);

            await _uow.SaveAsync();

            return toAdd.AsReadOnly();
        }
    }
}