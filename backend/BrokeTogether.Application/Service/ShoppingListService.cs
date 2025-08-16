// BrokeTogether.Application/Service/ShoppingListService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Application.Service.Contracts.Models;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Core.Enums;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IRepositoryManager _uow;
        private readonly ILogger<ShoppingListService> _logger;

        public ShoppingListService(IRepositoryManager repositoryManager, ILogger<ShoppingListService> logger)
        {
            _uow = repositoryManager;
            _logger = logger;
        }

        public async Task<ShoppingListItem> AddItemAsync(Guid homeId, string name, string createdByUserId)
        {
            await EnsureUserInHomeAsync(createdByUserId, homeId);

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            var item = new ShoppingListItem
            {
                HomeId = homeId,
                Name = name.Trim(),
                status = Status.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.ShoppingListRepository.AddItemAsync(item);
            await _uow.SaveAsync();

            _logger.LogInformation("Item '{Name}' added to home {HomeId} by {UserId}", item.Name, homeId, createdByUserId);
            return item;
        }

        public Task<IEnumerable<ShoppingListItem>> GetItemsAsync(Guid homeId)
            => _uow.ShoppingListRepository.GetAllItemsAsync(homeId, trackChanges: false);

        public Task<IEnumerable<ShoppingListItem>> GetPendingItemsAsync(Guid homeId)
            => _uow.ShoppingListRepository.GetPendingItemsAsync(homeId, trackChanges: false);

        public Task<IEnumerable<ShoppingListItem>> GetBoughtItemsAsync(Guid homeId)
            => _uow.ShoppingListRepository.GetBoughtItemsAsync(homeId, trackChanges: false);

        public async Task<ShoppingListItem?> RenameItemAsync(Guid itemId, string newName, string actingUserId)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("New name is required.", nameof(newName));

            var item = await _uow.ShoppingListRepository.GetItemByIdAsync(itemId, trackChanges: true);
            if (item is null)
                throw new InvalidOperationException($"Item '{itemId}' was not found.");

            await EnsureUserInHomeAsync(actingUserId, item.HomeId);

            item.Name = newName.Trim();
            await _uow.ShoppingListRepository.UpdateItemAsync(item);
            await _uow.SaveAsync();

            _logger.LogInformation("Item {ItemId} renamed to '{Name}' by {UserId}", itemId, item.Name, actingUserId);
            return item;
        }

        public async Task DeleteItemAsync(Guid itemId, string actingUserId)
        {
            var item = await _uow.ShoppingListRepository.GetItemByIdAsync(itemId, trackChanges: true);
            if (item is null)
                throw new InvalidOperationException($"Item '{itemId}' was not found.");

            await EnsureUserInHomeAsync(actingUserId, item.HomeId);

            await _uow.ShoppingListRepository.DeleteItemAsync(item);
            await _uow.SaveAsync();

            _logger.LogInformation("Item {ItemId} deleted by {UserId}", itemId, actingUserId);
        }

        public async Task<LinkItemsResult> MarkItemBoughtAndLinkToContributionAsync(
            Guid homeId,
            Guid itemId,
            string paidByUserId,
            string description,
            decimal amount,
            IEnumerable<SplitInput> splits)
        {
            await EnsureUserInHomeAsync(paidByUserId, homeId);

            var item = await _uow.ShoppingListRepository.GetItemByIdAsync(itemId, trackChanges: true);
            if (item is null)
                throw new InvalidOperationException($"Item '{itemId}' was not found.");
            if (item.HomeId != homeId)
                throw new InvalidOperationException("Item does not belong to the specified home.");

            // No-expense path: just mark bought
            if (amount <= 0m)
            {
                item.status = Status.Bought;
                item.ContributionId = null;
                await _uow.ShoppingListRepository.UpdateItemAsync(item);
                await _uow.SaveAsync();

                _logger.LogInformation("Item {ItemId} marked as bought without expense.", itemId);
                return new LinkItemsResult(null, new[] { itemId });
            }

            var normalizedDescription = string.IsNullOrWhiteSpace(description)
                ? item.Name
                : description.Trim();

            var resolvedSplits = await ResolveSplitsAsync(homeId, splits, amount, paidByUserId);

            var contribution = new Contribution
            {
                HomeId = homeId,
                PaidById = paidByUserId,
                Description = normalizedDescription,
                TotalAmount = amount,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.ContributionRepository.AddAsync(contribution);

            var splitEntities = resolvedSplits
                .Select(s => new ContributionSplit
                {
                    ContributionId = contribution.Id,
                    UserId = s.UserId,
                    AmountOwed = s.Amount
                })
                .ToArray();

            await _uow.ContributionSplitRepository.AddRangeAsync(splitEntities);

            item.status = Status.Bought;
            item.ContributionId = contribution.Id;
            await _uow.ShoppingListRepository.UpdateItemAsync(item);

            await _uow.SaveAsync();

            _logger.LogInformation(
                "Item {ItemId} linked to contribution {ContributionId} for home {HomeId}",
                itemId, contribution.Id, homeId
            );

            return new LinkItemsResult(contribution, new[] { itemId });
        }

        public async Task<LinkItemsResult> MarkItemsBoughtAndLinkToContributionAsync(
            Guid homeId,
            IEnumerable<Guid> itemIds,
            string paidByUserId,
            string description,
            decimal totalAmount,
            IEnumerable<SplitInput> splits)
        {
            if (itemIds is null) throw new ArgumentNullException(nameof(itemIds));
            var ids = itemIds.Distinct().ToArray();
            if (ids.Length == 0)
                throw new ArgumentException("At least one item is required.", nameof(itemIds));

            await EnsureUserInHomeAsync(paidByUserId, homeId);

            if (totalAmount <= 0m)
                throw new ArgumentException("Total amount must be positive.", nameof(totalAmount));

            var normalizedDescription = string.IsNullOrWhiteSpace(description)
                ? "Shopping Trip"
                : description.Trim();

            var items = new List<ShoppingListItem>(ids.Length);
            foreach (var id in ids)
            {
                var it = await _uow.ShoppingListRepository.GetItemByIdAsync(id, trackChanges: true);
                if (it is null)
                    throw new InvalidOperationException($"Item '{id}' was not found.");
                if (it.HomeId != homeId)
                    throw new InvalidOperationException($"Item '{id}' does not belong to the specified home.");
                items.Add(it);
            }

            var resolvedSplits = await ResolveSplitsAsync(homeId, splits, totalAmount, paidByUserId);

            var contribution = new Contribution
            {
                HomeId = homeId,
                PaidById = paidByUserId,
                Description = normalizedDescription,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow
            };
            await _uow.ContributionRepository.AddAsync(contribution);

            var splitEntities = resolvedSplits
                .Select(s => new ContributionSplit
                {
                    ContributionId = contribution.Id,
                    UserId = s.UserId,
                    AmountOwed = s.Amount
                })
                .ToArray();
            await _uow.ContributionSplitRepository.AddRangeAsync(splitEntities);

            foreach (var it in items)
            {
                it.status = Status.Bought;
                it.ContributionId = contribution.Id;
                await _uow.ShoppingListRepository.UpdateItemAsync(it);
            }

            await _uow.SaveAsync();

            _logger.LogInformation(
                "{Count} items linked to contribution {ContributionId} for home {HomeId}",
                items.Count, contribution.Id, homeId
            );

            return new LinkItemsResult(contribution, items.Select(i => i.Id).ToArray());
        }

        // ----------------- Helpers -----------------

        private async Task EnsureUserInHomeAsync(string userId, Guid homeId)
        {
            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            var home = await _uow.HomeRepository.GetByIdAsync(homeId, trackChanges: false);
            if (home is null)
                throw new InvalidOperationException($"Home '{homeId}' was not found.");

            if (!await _uow.HomeMemberRepository.IsUserInHomeAsync(userId, homeId))
                throw new UnauthorizedAccessException("User is not a member of this home.");
        }

        private async Task<IReadOnlyCollection<SplitInput>> ResolveSplitsAsync(
            Guid homeId,
            IEnumerable<SplitInput> splits,
            decimal totalAmount,
            string paidByUserId)
        {
            var provided = splits?.ToArray() ?? Array.Empty<SplitInput>();

            if (provided.Length == 0)
            {
                var members = await _uow.HomeMemberRepository.GetMembersByHomeIdAsync(homeId, trackChanges: false);
                var userIds = members.Select(m => m.UserId).Distinct().ToArray();
                if (userIds.Length == 0)
                    throw new InvalidOperationException("Home has no members to split with.");

                return EqualSplit(userIds, totalAmount);
            }

            var memberSet = (await _uow.HomeMemberRepository.GetMembersByHomeIdAsync(homeId, trackChanges: false))
                .Select(m => m.UserId)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var s in provided)
            {
                if (string.IsNullOrWhiteSpace(s.UserId))
                    throw new ArgumentException("Split user id is required.");
                if (!memberSet.Contains(s.UserId))
                    throw new UnauthorizedAccessException($"User '{s.UserId}' is not a member of this home.");
                if (s.Amount < 0m)
                    throw new ArgumentException("Split amount cannot be negative.");
            }

            var sum = provided.Sum(s => s.Amount);
            if (sum != totalAmount)
                throw new ArgumentException($"Sum of splits ({sum}) must equal total amount ({totalAmount}).");

            return provided;
        }

        private static IReadOnlyCollection<SplitInput> EqualSplit(IEnumerable<string> userIds, decimal total)
        {
            var ids = userIds.Distinct().ToArray();
            if (ids.Length == 0)
                throw new ArgumentException("At least one user is required for equal split.");

            var per = Math.Round(total / ids.Length, 2, MidpointRounding.AwayFromZero);
            var splits = new List<SplitInput>(ids.Length);
            decimal running = 0m;

            for (int i = 0; i < ids.Length; i++)
            {
                decimal amount = (i == ids.Length - 1)
                    ? Math.Round(total - running, 2, MidpointRounding.AwayFromZero)
                    : per;

                running += amount;
                splits.Add(new SplitInput(ids[i], amount));
            }

            return splits;
        }
    }
}