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
    public class ContributionRepository : RepositoryBase<Contribution>, IContributionRepository
    {
        public ContributionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task AddAsync(Contribution contribution)
        {
            await CreateAsync(contribution);
        }

        public Task DeleteAsync(Contribution contribution)
        {
            Delete(contribution);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Contribution>> GetAllByHomeIdAsync(Guid homeId, bool trackChanges=false)
        {
            return await FindByCondition(c => c.HomeId == homeId, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Contribution>> GetAllByPayerIdAsync(string userId,bool trackChanges=false)
        {
            return await FindByCondition(c => c.PaidById == userId, trackChanges).ToListAsync();
        }

        public async Task<Contribution?> GetByIdAsync(Guid id,bool trackChanges=false)
        {
            return await FindByCondition(c => c.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public Task UpdateAsync(Contribution contribution)
        {
            Update(contribution);
            return Task.CompletedTask;
        }
    }
}