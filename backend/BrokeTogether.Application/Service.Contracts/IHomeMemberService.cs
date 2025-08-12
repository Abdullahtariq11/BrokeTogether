// BrokeTogether.Application/Service/Contracts/IHomeMemberService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IHomeMemberService
    {
        /// <summary>
        /// Returns all members of a home (basic profile fields + role).
        /// </summary>
        Task<IEnumerable<HomeMember>> GetMembersAsync(Guid homeId);

        /// <summary>
        /// Adds a user to a home as a Member.
        /// Idempotent: if the user is already a member, should not duplicate.
        /// Returns the resulting HomeMember record.
        /// </summary>
        Task<HomeMember> AddMemberAsync(Guid homeId, string userId);

        /// <summary>
        /// Updates a member (e.g., role change).
        /// Requires admin privileges by the acting user.
        /// Returns the updated HomeMember record.
        /// </summary>
        Task<HomeMember> UpdateMemberRoleAsync(Guid homeId, string userId, string actingUserId, string newRole);

        /// <summary>
        /// Removes a member from a home.
        /// Requires admin privileges by the acting user.
        /// Should prevent removing the last Admin.
        /// </summary>
        Task RemoveMemberAsync(Guid homeId, string userId, string actingUserId);

        /// <summary>
        /// Checks if a user is a member of the home.
        /// </summary>
        Task<bool> IsUserInHomeAsync(Guid homeId, string userId);

        /// <summary>
        /// Checks if a user is an Admin of the home.
        /// </summary>
        Task<bool> IsAdminAsync(Guid homeId, string userId);
    }
}