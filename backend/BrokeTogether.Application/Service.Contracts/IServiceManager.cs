using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IServiceManager
    {
        public IContributionService ContributionService { get; }
        public IContributionSplitService ContributionSplitService { get; }
        public IHomeMemberService HomeMemberService { get; }
        public IHomeService HomeService { get; }
        public IShoppingListService ShoppingListService { get; }
        public IUserService UserService { get; }

    }
}
