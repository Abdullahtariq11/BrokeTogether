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
        Task<HomeMember?> GetByUserAndHomeAsync(string userId, Guid homeId); // Changed to Guid
        Task<IEnumerable<HomeMember>> GetMembersByHomeIdAsync(Guid homeId); // Changed to Guid
        Task<IEnumerable<HomeMember>> GetHomesByUserIdAsync(string userId);

        Task AddAsync(HomeMember member);
        Task RemoveAsync(HomeMember member);
        Task UpdateAsync(HomeMember member);

        Task<bool> IsUserInHomeAsync(string userId, Guid homeId); // Changed to Guid
        Task<bool> IsAdminAsync(string userId, Guid homeId); // Changed to Guid

    }
}