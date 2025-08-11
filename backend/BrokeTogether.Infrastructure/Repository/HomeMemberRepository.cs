using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Core.Enums;
using BrokeTogether.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokeTogether.Infrastructure.Repository
{
    public class HomeMemberRepository : RepositoryBase<HomeMember>, IHomeMemberRepository
    {
        public HomeMemberRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task AddMemberAsync(HomeMember member)
        {
            await CreateAsync(member);
        }

        public async Task<HomeMember?> GetByUserAndHomeAsync(string userId, Guid homeId, bool trackChanges = false)
        {
            return await FindByCondition(hm => hm.UserId == userId && hm.HomeId == homeId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<HomeMember>> GetMembersByHomeIdAsync(Guid homeId, bool trackChanges = false)
        {
            return await FindByCondition(hm => hm.HomeId == homeId, trackChanges).ToListAsync();
        }

        public async Task<bool> IsAdminAsync(string userId, Guid homeId)
        {
            return await FindByCondition(hm => hm.UserId == userId && hm.HomeId == homeId && hm.Role == Role.Admin, trackChanges: false).AnyAsync();

        }

        public async Task<bool> IsUserInHomeAsync(string userId, Guid homeId)
        {
            return await FindByCondition(hm => hm.UserId == userId && hm.HomeId == homeId, trackChanges: false).AnyAsync();
        }

        public Task RemoveMemberAsync(HomeMember member)
        {
            Delete(member);
            return Task.CompletedTask;
        }

        public Task UpdateMemberAsync(HomeMember member)
        {
            Update(member);
            return Task.CompletedTask;

        }
    }
}