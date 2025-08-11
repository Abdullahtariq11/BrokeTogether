using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Infrastructure.Data;

namespace BrokeTogether.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ContributionRepository> contributionRepository;
        private readonly Lazy<ContributionSplitRepository> contributionSplitRepository;
        private readonly Lazy<HomeMemberRepository> homeMemberRepository;
        private readonly Lazy<HomeRepository> homeRepository;
        private readonly Lazy<ShoppingListRepository> shoppingListRepository;
        private readonly Lazy<UserRepository> userRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            contributionRepository = new Lazy<ContributionRepository>(() => new ContributionRepository(_repositoryContext));
            contributionSplitRepository = new Lazy<ContributionSplitRepository>(() => new ContributionSplitRepository(_repositoryContext));
            homeMemberRepository = new Lazy<HomeMemberRepository>(() => new HomeMemberRepository(_repositoryContext));
            homeRepository = new Lazy<HomeRepository>(() => new HomeRepository(_repositoryContext));
            shoppingListRepository = new Lazy<ShoppingListRepository>(() => new ShoppingListRepository(_repositoryContext));
            userRepository = new Lazy<UserRepository>(() => new UserRepository(_repositoryContext));
        }

        public IContributionRepository ContributionRepository => contributionRepository.Value;

        public IContributionSplitRepository ContributionSplitRepository => contributionSplitRepository.Value;

        public IHomeMemberRepository HomeMemberRepository => homeMemberRepository.Value;

        public IHomeRepository HomeRepository => homeRepository.Value;

        public IShoppingListRepository ShoppingListRepository => shoppingListRepository.Value;

        public IUserRepository UserRepository => userRepository.Value;

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}