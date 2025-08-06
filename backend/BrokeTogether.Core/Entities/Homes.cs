using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Entities
{
    public class Homes
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "House name is required")]
        public string? Name { get; set; }
        public string? InviteCode { get; set; } //need to create algorithim for this
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}