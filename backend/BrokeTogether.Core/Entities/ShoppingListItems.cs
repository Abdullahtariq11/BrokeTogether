using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Enums;

namespace BrokeTogether.Core.Entities
{
    public class ShoppingListItems
    {
        public Guid Id { get; set; }
        public Guid HomeId { get; set; }
        [Required]
        public string? Name { get; set; }
        public Status status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
         public Guid? ContributionId { get; set; }

    }
}