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
    public class HomeRepository : RepositoryBase<Home>, IHomeRepository
    {
        public HomeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateHomeAsync(Home home)
        {
            await CreateAsync(home);
        }

        public Task DeleteHomeAsync(Home home)
        {
            Delete(home);
            return Task.CompletedTask;

        }

        public async Task<IEnumerable<Home>> GetAllHomeForUser(string id, bool trackChanges = false)
        {
            return await FindByCondition(h => h.Members.Any(m => m.UserId == id), trackChanges).ToListAsync();

        }

        public async Task<Home?> GetByIdAsync(Guid id, bool trackChanges = false)
        {
            return await FindByCondition(h => h.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Home?> GetHomeByInviteCodeAsync(string inviteCode, bool trackChanges = false)
        {
            return await FindByCondition(h => h.InviteCode == inviteCode, trackChanges).SingleOrDefaultAsync();
        }

        public Task UpdateHomeAsync(Home home)
        {
            Update(home);
            return Task.CompletedTask;
        }
    }
}