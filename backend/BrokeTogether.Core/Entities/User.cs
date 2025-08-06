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
    }
}