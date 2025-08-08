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
    public class UserRepository : RepositoryBase<User> , IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();

        }

        public async Task<User?> GetByEmailAsync(string email, bool trackChanges)
        {
            return await FindByCondition(u => u.Email == email, trackChanges).SingleOrDefaultAsync();

        }


        public async Task<User?> GetByIdAsync(string id, bool trackChanges)
        {
            return await FindByCondition(u => u.Id == id, trackChanges).SingleOrDefaultAsync();

        }


        public async Task<IEnumerable<User>> GetUsersByHomeId(Guid homeId, bool trackChanges)
        {
            return await FindByCondition(u => u.HomeMemeberships.Any(h => h.HomeId == homeId), trackChanges).ToListAsync();
        }


    }
}