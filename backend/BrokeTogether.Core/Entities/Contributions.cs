using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class Contributions
    {
        public Guid Id { get; set; }
        public Guid HomeId { get; set; }
        [Required]
        public string? PaidByIdId { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
         public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}