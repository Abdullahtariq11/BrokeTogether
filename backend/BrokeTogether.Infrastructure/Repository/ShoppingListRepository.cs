using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Core.Enums;
using BrokeTogether.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BrokeTogether.Infrastructure.Repository
{
    public class ShoppingListRepository : RepositoryBase<ShoppingListItem>, IShoppingListRepository
    {
        public ShoppingListRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task AddItemAsync(ShoppingListItem item)
        {
            await CreateAsync(item);
        }

        public Task DeleteItemAsync(ShoppingListItem item)
        {
            Delete(item);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ShoppingListItem>> GetAllItemsAsync(Guid homeId,bool trackChanges=false)
        {
            return await FindByCondition(s => s.HomeId == homeId, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<ShoppingListItem>> GetBoughtItemsAsync(Guid homeId,bool trackChanges=false)
        {
            return await FindByCondition(s => s.HomeId == homeId && s.status==Status.Bought, trackChanges).ToListAsync();
        }

        public async Task<ShoppingListItem?> GetItemByIdAsync(Guid itemId,bool trackChanges=false)
        {
            return await FindByCondition(s => s.Id == itemId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ShoppingListItem>> GetPendingItemsAsync(Guid homeId,bool trackChanges=false)
        {
            return await FindByCondition(s => s.HomeId == homeId && s.status==Status.Pending, trackChanges).ToListAsync();
        }

        public async Task LinkItemsToContributionAsync(IEnumerable<Guid> itemIds, Guid contributionId)
        {
            var ids = itemIds.ToArray();
            var items = await _repositoryContext.Set<ShoppingListItem>().Where(i => ids.Contains(i.Id)).ToListAsync();

            foreach (var item in items)
            {
                item.ContributionId = contributionId;
                item.status = Status.Bought;
            }
          UpdateRangeAsync(items);
        }

        public Task UpdateItemAsync(ShoppingListItem item)
        {
            Update(item);
            return Task.CompletedTask;
        }
    }
}