using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Enums;

namespace BrokeTogether.Core.Entities
{
    public class ShoppingListItem
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public Status status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Foreign key for the Home this item belongs to
        public Guid HomeId { get; set; }
        // Navigation property back to the Home
        public Home Home { get; set; } = null!;

        // Foreign key for the Contribution (nullable, as it's only set after purchase)
        public Guid? ContributionId { get; set; }
        // Navigation property to the Contribution
        public Contribution? Contribution { get; set; }

    }
}