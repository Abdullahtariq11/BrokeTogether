using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class ContributionSplits
    {
        public Guid Id { get; set; }
        public Guid ContributionId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public decimal AmountOwed { get; set; }
    }
}