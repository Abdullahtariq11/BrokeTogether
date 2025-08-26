using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IServiceManager
    {
        IAuthService Auth { get; }
        IHomeService Homes { get; }
        IHomeMemberService HomeMembers { get; }
        IShoppingListService ShoppingList { get; }
        IContributionService Contributions { get; }
        IContributionSplitService ContributionSplits { get; }
        IUserService Users { get; } // keep for later (can be a thin wrapper over Identity)

    }
}
