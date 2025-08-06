using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Enums;

namespace BrokeTogether.Core.Entities
{
    public class HomeMembers
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Guid HomeId { get; set; } //ForeignKey
        [Required]
        public string? UserId { get; set; } //ForeignKey

        // Navigation property back to the User
        public User User { get; set; } = null!;
        // Navigation property back to the Home
        public Home Home { get; set; } = null!;

    }
}