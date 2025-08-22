using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Application.DTOs.Auth
{
    public record AuthResult(
        bool Succeeded,
        string? AccessToken,
        DateTime? AccessTokenExpiresAt,
        string? RefreshToken,
        DateTime? RefreshTokenExpiresAt,
        UserSummary? User,
        IReadOnlyList<string>? Errors
    );
}