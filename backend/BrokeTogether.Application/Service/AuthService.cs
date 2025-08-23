using System.Security.Claims;
using BrokeTogether.Application.DTOs.Auth;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ITokenService tokenService,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AuthService> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(RegisterUserDto dto)
        {
            if (dto is null) throw new Exception("Invalid payload.");
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Email and password are required.");

            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing is not null)
                throw new Exception("Email is already in use.");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };

            var create = await _userManager.CreateAsync(user, dto.Password);
            if (!create.Succeeded)
            {
                var firstError = create.Errors.FirstOrDefault()?.Description ?? "Registration failed.";
                throw new Exception(firstError);
            }

            // (Optional) Add claims/roles via UserManager here

            var (access, accessExp) = await _tokenService.CreateAccessTokenAsync(user, Enumerable.Empty<Claim>());
            var (refresh, refreshExp) = await _tokenService.CreateRefreshTokenAsync(user);

            return new AuthResult(
                Succeeded: true,
                AccessToken: access,
                AccessTokenExpiresAt: accessExp,
                RefreshToken: refresh,
                RefreshTokenExpiresAt: refreshExp,
                User: new UserSummary(user.Id, user.Email, user.FullName),
                Errors: new List<string>()
            );
        }

        public async Task<AuthResult> LoginAsync(LoginUserDto dto)
        {
            if (dto is null) throw new Exception("Invalid payload.");
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Invalid email or password.");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                throw new Exception("Invalid credentials.");

            // For MVP/dev, avoid surprise lockouts -> lockoutOnFailure: false
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                var reason = result.IsLockedOut ? "Account is locked." :
                             result.RequiresTwoFactor ? "Two-factor authentication required." :
                             "Invalid credentials.";
                throw new Exception(reason);
            }

            var (access, accessExp) = await _tokenService.CreateAccessTokenAsync(user, Enumerable.Empty<Claim>());
            var (refresh, refreshExp) = await _tokenService.CreateRefreshTokenAsync(user);

            return new AuthResult(
                Succeeded: true,
                AccessToken: access,
                AccessTokenExpiresAt: accessExp,
                RefreshToken: refresh,
                RefreshTokenExpiresAt: refreshExp,
                User: new UserSummary(user.Id, user.Email, user.FullName),
                Errors: new List<string>()
            );
        }

        public async Task<AuthResult> RefreshAsync(RefreshTokenRequest dto)
        {
            if (dto is null || string.IsNullOrWhiteSpace(dto.RefreshToken))
                throw new Exception("Refresh token is required.");

            var (isValid, userId, _, error) = await _tokenService.ValidateRefreshTokenAsync(dto.RefreshToken);
            if (!isValid || string.IsNullOrWhiteSpace(userId))
                throw new Exception(error ?? "Invalid refresh token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new Exception("User not found.");

            await _tokenService.RevokeRefreshTokensAsync(user.Id);

            var (access, accessExp) = await _tokenService.CreateAccessTokenAsync(user, Enumerable.Empty<Claim>());
            var (refresh, refreshExp) = await _tokenService.CreateRefreshTokenAsync(user);

            return new AuthResult(
                Succeeded: true,
                AccessToken: access,
                AccessTokenExpiresAt: accessExp,
                RefreshToken: refresh,
                RefreshTokenExpiresAt: refreshExp,
                User: new UserSummary(user.Id, user.Email, user.FullName),
                Errors: new List<string>()
            );
        }

        public Task<bool> RevokeRefreshTokensAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.FromResult(false);

            return _tokenService.RevokeRefreshTokensAsync(userId);
        }
    }
}