using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Contracts
{
    public interface IRepositoryManager
    {
        IContributionRepository ContributionRepository { get; }
        IContributionSplitRepository ContributionSplitRepository { get; }
        IHomeMemberRepository HomeMemberRepository { get; }
        IHomeRepository HomeRepository { get; }
        IShoppingListRepository ShoppingListRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}