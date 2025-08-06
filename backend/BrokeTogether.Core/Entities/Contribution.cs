using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class Contribution
    {
        public Guid Id { get; set; }
        public Guid HomeId { get; set; }
        [Required]
        public string? PaidByIdId { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation property back to the Home
        public Home Home { get; set; } = null!;
        // Navigation property to the User who paid
        public User PaidBy { get; set; } = null!;
        // A contribution is broken down into multiple splits.

        public ICollection<ContributionSplit> Splits { get; set; } = new List<ContributionSplit>();

        // A contribution can be linked to the shopping items purchased in that trip.
        public ICollection<ShoppingListItem> PurchasedItems { get; set; } = new List<ShoppingListItem>();

    }
}