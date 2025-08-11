using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IContributionRepository
    {
        Task<Contribution?> GetByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Contribution>> GetAllByHomeIdAsync(Guid homeId, bool trackChanges); // Changed to Guid
        Task<IEnumerable<Contribution>> GetAllByPayerIdAsync(string userId, bool trackChanges); // Renamed for clarity
        Task AddAsync(Contribution contribution);
        Task UpdateAsync(Contribution contribution);
        Task DeleteAsync(Contribution contribution);

    }
}