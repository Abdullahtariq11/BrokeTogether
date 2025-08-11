using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IHomeMemberRepository
    {
        // Removed GetByIdAsync as this entity has a composite key.
        Task<HomeMember?> GetByUserAndHomeAsync(string userId, Guid homeId,bool trackChanges); // Changed to Guid
        Task<IEnumerable<HomeMember>> GetMembersByHomeIdAsync(Guid homeId,bool trackChanges); // Changed to Guid
        Task AddMemberAsync(HomeMember member);
        Task RemoveMemberAsync(HomeMember member);
        Task UpdateMemberAsync(HomeMember member);

        Task<bool> IsUserInHomeAsync(string userId, Guid homeId); // Changed to Guid
        Task<bool> IsAdminAsync(string userId, Guid homeId); // Changed to Guid

    }
}