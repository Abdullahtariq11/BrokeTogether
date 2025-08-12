// BrokeTogether.Application/Service/Contracts/IHomeService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IHomeService
    {
        /// <summary>
        /// Creates a new home and makes the creator an Admin member.
        /// Returns the created Home.
        /// </summary>
        Task<Home> CreateHomeAsync(string userId, string name);

        /// <summary>
        /// Gets a single home by its Id.
        /// </summary>
        Task<Home?> GetHomeAsync(Guid homeId);

        /// <summary>
        /// Returns all homes the user is a member of.
        /// </summary>
        Task<IEnumerable<Home>> GetHomesForUserAsync(string userId);

        /// <summary>
        /// Gets a home by its invite code.
        /// </summary>
        Task<Home?> GetHomeByInviteCodeAsync(string code);

        /// <summary>
        /// Joins a home using an invite code. 
        /// If the user is already a member, should behave idempotently.
        /// Returns the joined Home.
        /// </summary>
        Task<Home> JoinHomeByInviteCodeAsync(string userId, string code);
    }
}