using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.DTOs.Contribution
{
    public record UpdateContributionDto(Guid Id,string? PaidById, string? Description, decimal TotalAmount);
}