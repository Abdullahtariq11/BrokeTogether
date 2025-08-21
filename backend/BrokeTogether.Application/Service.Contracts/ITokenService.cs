using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface ITokenService
    {
        /// <summary>
        /// Low-level token operations (build, sign, validate).
        /// Keep crypto & JWT-specifics here, away from IAuthService orchestration.
        /// </summary>
        Task<(string accessToken, DateTime expiresAt)> CreateAccessTokenAsync(User user, IEnumerable<Claim> extraClaims);
        Task<(string refreshToken, DateTime expiresAt)> CreateRefreshTokenAsync(User user);
        Task<(bool isValid, string? userId, IEnumerable<Claim>? claims, string? error)> ValidateRefreshTokenAsync(string refreshToken);
        Task<bool> RevokeRefreshTokensAsync(string userId); // e.g., rotate or blacklist

    }
}