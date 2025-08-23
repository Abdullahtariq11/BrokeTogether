using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.DTOs.Auth
{
    public record RegisterUserDto(string FullName, string Email, string Password);
}