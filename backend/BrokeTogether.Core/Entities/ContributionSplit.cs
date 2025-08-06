using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class ContributionSplit
    {
        public Guid Id { get; set; }
        public Guid ContributionId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public decimal AmountOwed { get; set; }

        // Navigation property back to the Contribution
        public Contribution Contribution { get; set; } = null!;
        // Navigation property to the User
        public User User { get; set; } = null!;


    }
}