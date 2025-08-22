using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Shared.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace BrokeTogether.Application.Service
{
    public class TokenService : ITokenService
    {

        private readonly JwtSettings _jwtSettings;
        private SymmetricSecurityKey _securityKey;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        }


        public Task<(string accessToken, DateTime expiresAt)> CreateAccessTokenAsync(User user, IEnumerable<Claim> extraClaims)
        {
            var now = DateTime.UtcNow;
            var expiresAt = now.AddMinutes(_jwtSettings.AccessTokenMinutes);
            //set claims
            var authClaims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub,user.Id),
                    new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new(ClaimTypes.NameIdentifier, user.Id),
                new("typ", "access")
            };
            if (extraClaims != null)
                authClaims.AddRange(extraClaims);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: authClaims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256)
            );
            return Task.FromResult((new JwtSecurityTokenHandler().WriteToken(token), expiresAt));
        }

        public Task<(string refreshToken, DateTime expiresAt)> CreateRefreshTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeRefreshTokensAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isValid, string? userId, IEnumerable<Claim>? claims, string? error)> ValidateRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}