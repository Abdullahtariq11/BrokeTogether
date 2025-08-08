using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Core.Contracts
{
    public interface IShoppingListRepository
    {
        Task<IEnumerable<ShoppingListItem>> GetAllItemsAsync(Guid homeId); // Changed to Guid
        Task<IEnumerable<ShoppingListItem>> GetPendingItemsAsync(Guid homeId); // Changed to Guid
        Task<IEnumerable<ShoppingListItem>> GetBoughtItemsAsync(Guid homeId); // Changed to Guid
        Task<ShoppingListItem?> GetItemByIdAsync(int itemId);
        Task AddItemAsync(ShoppingListItem item);
        Task UpdateItemAsync(ShoppingListItem item);
        Task DeleteItemAsync(ShoppingListItem item); // Changed parameter to entity for safety
        Task LinkItemsToContributionAsync(IEnumerable<int> itemIds, int contributionId); // Changed to handle multiple items

    }
}