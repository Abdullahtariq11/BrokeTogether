using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Contribution;
using BrokeTogether.Application.DTOs.ShoppingList;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IContributionService
    {
        Task<Contribution?> GetByIdAsync(Guid id);
        Task<IEnumerable<Contribution>> GetAllByHomeIdAsync(Guid homeId);
        Task<IEnumerable<Contribution>> GetAllByPayerIdAsync(string userId);
        Task AddAsync(ContributionCreateDto contributionDto);
        Task UpdateAsync(UpdateContributionDto contributionDto);
        Task DeleteAsync(Guid id);
    }
}