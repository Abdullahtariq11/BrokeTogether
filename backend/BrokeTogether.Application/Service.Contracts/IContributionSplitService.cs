// BrokeTogether.Application/Service/Contracts/IContributionSplitService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts.Models;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    /// <summary>
    /// Application-layer contract for managing contribution splits (who owes what).
    /// </summary>
    public interface IContributionSplitService
    {
        /// <summary>
        /// Gets a single split for a specific user within a contribution.
        /// </summary>
        Task<ContributionSplit?> GetByIdsAsync(Guid contributionId, string userId);

        /// <summary>
        /// Gets all splits for a contribution.
        /// </summary>
        Task<IEnumerable<ContributionSplit>> GetByContributionIdAsync(Guid contributionId);

        /// <summary>
        /// Gets all splits across contributions for a given user.
        /// </summary>
        Task<IEnumerable<ContributionSplit>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Adds splits to a contribution without removing existing ones.
        /// </summary>
        Task<IReadOnlyCollection<ContributionSplit>> AddSplitsAsync(
            Guid contributionId,
            IEnumerable<SplitInput> splits);

        /// <summary>
        /// Replaces all splits for a contribution with the provided set (remove then add).
        /// </summary>
        Task<IReadOnlyCollection<ContributionSplit>> SetSplitsForContributionAsync(
            Guid contributionId,
            IEnumerable<SplitInput> splits);

        /// <summary>
        /// Updates the amount owed for a specific user's split within a contribution.
        /// </summary>
        Task<ContributionSplit?> UpdateSplitAsync(Guid contributionId, string userId, decimal amount);

        /// <summary>
        /// Removes a specific user's split from a contribution.
        /// </summary>
        Task RemoveSplitAsync(Guid contributionId, string userId);
    }
    

}