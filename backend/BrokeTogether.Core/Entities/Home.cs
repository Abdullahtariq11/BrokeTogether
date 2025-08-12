using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class Home
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "House name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Invite code  is required")]
        public string? InviteCode { get; set; } //need to create algorithim for this
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // --- Navigation Properties ---

    // A home has a collection of members.
    public ICollection<HomeMember> Members { get; set; } = new List<HomeMember>();

    // A home has a history of all its contributions.
    public ICollection<Contribution> Contributions { get; set; } = new List<Contribution>();

    // A home has a list of all its shopping items.
    public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();

    }
}