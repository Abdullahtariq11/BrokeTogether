using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IContributionSplitRepository
    {
        Task<ContributionSplit?> GetByIdsAsync(int contributionId, string userId); // Added method to get by composite key
        Task<IEnumerable<ContributionSplit>> GetByContributionIdAsync(int contributionId);
        Task<IEnumerable<ContributionSplit>> GetByUserIdAsync(string userId);
        Task AddRangeAsync(IEnumerable<ContributionSplit> splits); // Changed to AddRange for efficiency
        Task UpdateAsync(ContributionSplit split);
        Task DeleteAsync(ContributionSplit split);

    }
}