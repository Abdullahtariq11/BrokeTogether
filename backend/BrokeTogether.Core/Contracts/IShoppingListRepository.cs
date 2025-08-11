using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IShoppingListRepository
    {
        Task<IEnumerable<ShoppingListItem>> GetAllItemsAsync(Guid homeId,bool trackChanges); // Changed to Guid
        Task<IEnumerable<ShoppingListItem>> GetPendingItemsAsync(Guid homeId,bool trackChanges); // Changed to Guid
        Task<IEnumerable<ShoppingListItem>> GetBoughtItemsAsync(Guid homeId,bool trackChanges); // Changed to Guid
        Task<ShoppingListItem?> GetItemByIdAsync(Guid itemId,bool trackChanges);
        Task AddItemAsync(ShoppingListItem item);
        Task UpdateItemAsync(ShoppingListItem item);
        Task DeleteItemAsync(ShoppingListItem item); // Changed parameter to entity for safety
        Task LinkItemsToContributionAsync(IEnumerable<Guid> itemIds, Guid contributionId); // Changed to handle multiple items

    }
}