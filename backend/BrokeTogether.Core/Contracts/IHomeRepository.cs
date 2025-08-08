using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IHomeRepository
    {
        Task<Home?> GetByIdAsync(Guid id);
        Task<IEnumerable<Home>> GetAllHomeForUser(string id);
        Task CreateHomeAsync(Home home);
        Task UpdateHomeAsync(Home home);
        Task DeleteHomeAsync(Home home);
        Task<Home?> GetHomeByInviteCodeAsync(string inviteCode);


    }
}