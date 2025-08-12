// BrokeTogether.Application/Service/Contracts/IShoppingListService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts.Models;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    /// <summary>
    /// Application layer contract for managing shopping list items and the flow
    /// that converts purchased items into a contribution with splits.
    /// </summary>
    public interface IShoppingListService
    {
        /// <summary>
        /// Adds a new item to a home's shopping list.
        /// </summary>
        Task<ShoppingListItem> AddItemAsync(Guid homeId, string name, string createdByUserId);

        /// <summary>
        /// Returns all shopping list items for a home (both pending and bought).
        /// </summary>
        Task<IEnumerable<ShoppingListItem>> GetItemsAsync(Guid homeId);

        /// <summary>
        /// Returns only items that are still pending (not bought).
        /// </summary>
        Task<IEnumerable<ShoppingListItem>> GetPendingItemsAsync(Guid homeId);

        /// <summary>
        /// Returns items that have been marked as bought.
        /// </summary>
        Task<IEnumerable<ShoppingListItem>> GetBoughtItemsAsync(Guid homeId);

        /// <summary>
        /// Renames an existing shopping list item. Returns the updated entity.
        /// </summary>
        Task<ShoppingListItem?> RenameItemAsync(Guid itemId, string newName, string actingUserId);

        /// <summary>
        /// Deletes a shopping list item.
        /// </summary>
        Task DeleteItemAsync(Guid itemId, string actingUserId);

        /// <summary>
        /// Marks the provided items as bought and links them to a newly created contribution.
        /// Also records the contribution's splits.
        /// Returns the created contribution and the set of item ids that were linked.
        /// </summary>
        Task<LinkItemsResult> MarkItemsBoughtAndLinkToContributionAsync(
            Guid homeId,
            IEnumerable<Guid> itemIds,
            string paidByUserId,
            string description,
            decimal totalAmount,
            IEnumerable<SplitInput> splits);

        /// <summary>
        /// Convenience overload for a single item—wraps the bulk method.
        /// </summary>
        Task<LinkItemsResult> MarkItemBoughtAndLinkToContributionAsync(
            Guid homeId,
            Guid itemId,
            string paidByUserId,
            string description,
            decimal amount,
            IEnumerable<SplitInput> splits);
    }

    /// <summary>
    /// Result of linking items to a contribution.
    /// </summary>
    public record LinkItemsResult(Contribution Contribution, IReadOnlyCollection<Guid> LinkedItemIds);
}