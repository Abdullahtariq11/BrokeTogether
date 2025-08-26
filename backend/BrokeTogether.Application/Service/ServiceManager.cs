using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BrokeTogether.Application.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _auth;
        private readonly Lazy<IHomeService> _homes;
        private readonly Lazy<IHomeMemberService> _homeMembers;
        private readonly Lazy<IShoppingListService> _shoppingList;
        private readonly Lazy<IContributionService> _contributions;
        private readonly Lazy<IContributionSplitService> _contributionSplits;
        private readonly Lazy<IUserService> _users;

        public ServiceManager(IServiceProvider sp)
        {
            _auth = new(() => sp.GetRequiredService<IAuthService>());
            _homes = new(() => sp.GetRequiredService<IHomeService>());
            _homeMembers = new(() => sp.GetRequiredService<IHomeMemberService>());
            _shoppingList = new(() => sp.GetRequiredService<IShoppingListService>());
            _contributions = new(() => sp.GetRequiredService<IContributionService>());
            _contributionSplits = new(() => sp.GetRequiredService<IContributionSplitService>());
            _users = new(() => sp.GetRequiredService<IUserService>());
        }

        public IAuthService Auth => _auth.Value;
        public IHomeService Homes => _homes.Value;
        public IHomeMemberService HomeMembers => _homeMembers.Value;
        public IShoppingListService ShoppingList => _shoppingList.Value;
        public IContributionService Contributions => _contributions.Value;
        public IContributionSplitService ContributionSplits => _contributionSplits.Value;
        public IUserService Users => _users.Value;
    }
}