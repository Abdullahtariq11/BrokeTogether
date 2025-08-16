using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.DTOs.ShoppingList
{
    public record ShoppingListItemCreateDto(Guid HomeId,string PaidById, string? Description, decimal TotalAmount);
}