using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokeTogether.Infrastructure.Repository
{
    public class ContributionSplitRepository : RepositoryBase<ContributionSplit>, IContributionSplitRepository
    {
        public ContributionSplitRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task AddRangeAsync(IEnumerable<ContributionSplit> splits)
        {
            await CreateRangeAsync(splits);
        }

        public Task DeleteAsync(ContributionSplit split)
        {
            Delete(split);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ContributionSplit>> GetByContributionIdAsync(Guid contributionId,bool trackChanges=false)
        {
            return await FindByCondition(cs => cs.ContributionId == contributionId, trackChanges).ToListAsync();
        }

        public async Task<ContributionSplit?> GetByIdsAsync(Guid contributionId, string userId,bool trackChanges=false)
        {
            return await FindByCondition(cs => cs.ContributionId == contributionId && cs.UserId==userId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ContributionSplit>> GetByUserIdAsync(string userId,bool trackChanges=false)
        {
             return await FindByCondition(cs => cs.UserId==userId, trackChanges).ToListAsync();
        }

        public Task UpdateAsync(ContributionSplit split)
        {
            Update(split);
            return Task.CompletedTask;
        }
    }
}