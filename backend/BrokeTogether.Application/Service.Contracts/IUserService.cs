// BrokeTogether.Application/Service/Contracts/IUserService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Auth;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    /// <summary>
    /// Application-layer contract for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        Task<UserSummary?> GetUserDetailByIdAsync(string userId);

        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Gets all users in the system.
        /// </summary>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Gets all users who are members of a specific home.
        /// </summary>
        Task<IEnumerable<User>> GetUsersByHomeIdAsync(Guid homeId);

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        Task<User?> UpdateUserAsync(User user);


    }
}