using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.DTOs.Home
{
    public record HomeDto(Guid Id, string Name, string InviteCode);

}