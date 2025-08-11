using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IContributionSplitRepository
    {
        Task<ContributionSplit?> GetByIdsAsync(Guid contributionId, string userId,bool trackChanges); // Added method to get by composite key
        Task<IEnumerable<ContributionSplit>> GetByContributionIdAsync(Guid contributionId,bool trackChanges);
        Task<IEnumerable<ContributionSplit>> GetByUserIdAsync(string userId,bool trackChanges);
        Task AddRangeAsync(IEnumerable<ContributionSplit> splits); // Changed to AddRange for efficiency
        Task UpdateAsync(ContributionSplit split);
        Task DeleteAsync(ContributionSplit split);

    }
}