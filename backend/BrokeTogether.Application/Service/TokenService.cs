using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Concurrent;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Shared.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BrokeTogether.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SymmetricSecurityKey _securityKey;

        // MVP in-memory store (thread-safe). Replace with persistent store later.
        private static readonly ConcurrentDictionary<string, (string token, DateTime expires)> _refreshStore
            = new ConcurrentDictionary<string, (string token, DateTime expires)>();

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        }

        public Task<(string accessToken, DateTime expiresAt)> CreateAccessTokenAsync(User user, IEnumerable<Claim> extraClaims)
        {
            var now = DateTime.UtcNow;
            var expiresAt = now.AddMinutes(_jwtSettings.AccessTokenMinutes);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new (ClaimTypes.NameIdentifier, user.Id),
                new ("typ", "access")
            };

            if (extraClaims is not null)
                claims.AddRange(extraClaims);

            var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult((jwt, expiresAt));
        }

        public Task<(string refreshToken, DateTime expiresAt)> CreateRefreshTokenAsync(User user)
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            var token = Convert.ToBase64String(bytes);
            var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);

            _refreshStore[user.Id] = (token, expiresAt);
            return Task.FromResult((token, expiresAt));
        }

        public Task<bool> RevokeRefreshTokensAsync(string userId)
        {
            var removed = _refreshStore.TryRemove(userId, out _);
            return Task.FromResult(removed);
        }

        public Task<(bool isValid, string? userId, IEnumerable<Claim>? claims, string? error)> ValidateRefreshTokenAsync(string refreshToken)
        {
            // MVP: reverse-lookup by value (O(n)); replace with persistent store per user/device later
            foreach (var kvp in _refreshStore)
            {
                var userId = kvp.Key;
                var (stored, exp) = kvp.Value;

                if (string.Equals(stored, refreshToken, StringComparison.Ordinal))
                {
                    if (DateTime.UtcNow >= exp)
                        return Task.FromResult((false, (string?)null, (IEnumerable<Claim>?)null, "Refresh token expired."));

                    return Task.FromResult((true, userId, (IEnumerable<Claim>?)null, (string?)null));
                }
            }

            return Task.FromResult((false, (string?)null, (IEnumerable<Claim>?)null, "Refresh token not found."));
        }
    }
}