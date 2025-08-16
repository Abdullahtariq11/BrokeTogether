using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Contribution;
using BrokeTogether.Application.DTOs.ShoppingList;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class ContributionService : IContributionService
    {
        private readonly IRepositoryManager _uow;
        private readonly ILogger<ContributionService> _logger;

        public ContributionService(IRepositoryManager repositoryManager, ILogger<ContributionService> logger)
        {
            _uow = repositoryManager;
            _logger = logger;
        }

        public async Task AddAsync(ContributionCreateDto contributionDto)
        {
            _logger.LogInformation("Creating Contribution");

            if (contributionDto is null)
                throw new ArgumentNullException(nameof(contributionDto));

            if (contributionDto.HomeId == Guid.Empty)
                throw new ArgumentException("HomeId is required.", nameof(contributionDto.HomeId));

            if (string.IsNullOrWhiteSpace(contributionDto.PaidById))
                throw new ArgumentException("PaidById is required.", nameof(contributionDto.PaidById));

            if (await _uow.HomeRepository.GetByIdAsync(contributionDto.HomeId, false) is null)
                throw new InvalidOperationException($"No home exists with id: {contributionDto.HomeId}");

            if (await _uow.UserRepository.GetByIdAsync(contributionDto.PaidById, false) is null)
                throw new InvalidOperationException($"No user exists with id: {contributionDto.PaidById}");

            var contribution = new Contribution
            {
                HomeId = contributionDto.HomeId,
                PaidById = contributionDto.PaidById,
                Description = contributionDto.Description,
                TotalAmount = contributionDto.TotalAmount
            };

            await _uow.ContributionRepository.AddAsync(contribution);
            await _uow.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting Contribution");

            var contribution = await _uow.ContributionRepository.GetByIdAsync(id, true);
            if (contribution is null)
                throw new InvalidOperationException($"No contribution exists with id: {id}");

            await _uow.ContributionRepository.DeleteAsync(contribution);
            await _uow.SaveAsync();

        }

        public async Task<IEnumerable<Contribution>> GetAllByHomeIdAsync(Guid homeId)
        {
            _logger.LogInformation("Get all the Contributions for homeid: {homeId}",homeId);

            var contributions = await _uow.ContributionRepository.GetAllByHomeIdAsync(homeId, false);
            if (contributions.Count() <= 0)
                throw new InvalidOperationException($"No contribution exists for home with id: {homeId}");
            return contributions;
        }

        public async Task<IEnumerable<Contribution>> GetAllByPayerIdAsync(string userId)
        {
            _logger.LogInformation("Get all the Contributions");

            var contributions = await _uow.ContributionRepository.GetAllByPayerIdAsync(userId, false);
            if (contributions.Count() <= 0)
                throw new InvalidOperationException($"No contribution exists for user with id: {userId}");
            return contributions;
        }

        public async Task<Contribution?> GetByIdAsync(Guid id)
        {
            return await _uow.ContributionRepository.GetByIdAsync(id, false);

        }

        public async Task UpdateAsync(UpdateContributionDto contributionDto)
        {
            _logger.LogInformation("Creating Contribution");

            if (contributionDto is null)
                throw new ArgumentNullException(nameof(contributionDto));

            var contribution = await _uow.ContributionRepository.GetByIdAsync(contributionDto.Id, false);
            if (contribution is null)
                throw new InvalidOperationException($"No contrinution exists with id: {contributionDto.Id}");

            if (contribution is null)
                throw new InvalidOperationException($"No contrinution exists with id: {contributionDto.Id}");

            if (await _uow.UserRepository.GetByIdAsync(contributionDto.PaidById, false) is null)
                throw new InvalidOperationException($"No user exists with id: {contributionDto.PaidById}");

            if (!string.Equals(contribution.Description, contributionDto.Description))
                contribution.Description = contributionDto.Description;

            if (!string.Equals(contribution.PaidById, contributionDto.PaidById))
                contribution.PaidById = contributionDto.PaidById;

            if (contribution.TotalAmount != contributionDto.TotalAmount)
                contribution.TotalAmount = contributionDto.TotalAmount;

            await _uow.ContributionRepository.UpdateAsync(contribution);
            await _uow.SaveAsync();
            

        }
    }
}