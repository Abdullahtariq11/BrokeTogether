using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BrokeTogether.Core.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "This Field is required")]
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation Property
        // A user can be a member of many homes.
        public ICollection<HomeMembers> HomeMemebership { get; set; } = new List<HomeMembers>();
        // A user can pay for many contributions.
        public ICollection<Contribution> ContributionsPaid { get; set; } = new List<Contribution>();
        // A user can be responsible for many individual splits.
        public ICollection<ContributionSplit> OwedSplits { get; set; } = new List<ContributionSplit>();

    }
}