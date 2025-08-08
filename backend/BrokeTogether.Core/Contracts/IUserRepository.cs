using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;


namespace BrokeTogether.Core.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id,bool trackChanges); 
        Task<User?> GetByEmailAsync(string email,bool trackChanges);
        Task<IEnumerable<User>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<User>> GetUsersByHomeId(Guid homeId,bool trackChanges); // Get all user for a home

    }
}