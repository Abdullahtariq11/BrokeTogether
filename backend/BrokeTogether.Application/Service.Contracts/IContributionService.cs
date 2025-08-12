using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IContributionService
    {
        Task<Contribution?> GetByIdAsync(Guid id);
        Task<IEnumerable<Contribution>> GetAllByHomeIdAsync(Guid homeId);
        Task<IEnumerable<Contribution>> GetAllByPayerIdAsync(string userId);
        Task AddAsync(Contribution contribution);
        Task UpdateAsync(Contribution contribution);
        Task DeleteAsync(Guid id);
    }
}