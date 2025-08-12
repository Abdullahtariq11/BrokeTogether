using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.Service.Contracts.Models
{
       /// <summary>
    /// Input model representing a user's owed amount in a contribution.
    /// </summary>
    public record SplitInput(string UserId, decimal Amount);
}