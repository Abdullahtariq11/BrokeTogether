using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

/// <summary>
/// Defines the contract for user repository operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetByIdAsync(string id);

    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all users.</returns>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Retrieves all users associated with a specific home asynchronously.
    /// </summary>
    /// <param name="homeId">The unique identifier of the home.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of users belonging to the specified home.</returns>
    Task<IEnumerable<User>> GetUsersByHomeId(Guid homeId);
}
namespace BrokeTogether.Core.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id); 
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetUsersByHomeId(Guid homeId); // Get all user for a home


    }
}