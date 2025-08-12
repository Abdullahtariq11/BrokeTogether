// BrokeTogether.Application/Service/HomeService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Core.Enums;
using BrokeTogether.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class HomeService : IHomeService
    {
        private readonly IRepositoryManager _uow;
        private readonly ILogger<HomeService> _logger;

        public HomeService(IRepositoryManager repositoryManager, ILogger<HomeService> logger)
        {
            _uow = repositoryManager;
            _logger = logger;
        }

        public async Task<Home> CreateHomeAsync(string userId, string name)
        {
            _logger.LogInformation("Creating home '{Name}' for user {UserId}", name, userId);

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Home name is required.", nameof(name));

            // Ensure user exists (trackChanges: false since we don't modify the user here)
            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            // Generate unique invite code
            var code = await GenerateUniqueInviteCodeAsync();

            var home = new Home
            {
                Name = name.Trim(),
                InviteCode = code
            };

            await _uow.HomeRepository.CreateHomeAsync(home);

            // Add creator as Admin
            var member = new HomeMember
            {
                HomeId = home.Id,
                UserId = userId,
                Role = Role.Admin
            };
            await _uow.HomeMemberRepository.AddMemberAsync(member);

            await _uow.SaveAsync();

            _logger.LogInformation("Home '{HomeId}' created with invite code {Code}", home.Id, code);
            return home;
        }

        public async Task<Home?> GetHomeAsync(Guid homeId)
        {
            var home = await _uow.HomeRepository.GetByIdAsync(homeId, trackChanges: false);
            return home; // return null if not found
        }

        public async Task<IEnumerable<Home>> GetHomesForUserAsync(string userId)
        {
            // Returns empty sequence if user has no homes — not an error
            var homes = await _uow.HomeRepository.GetAllHomeForUser(userId, trackChanges: false);
            return homes;
        }

        public async Task<Home?> GetHomeByInviteCodeAsync(string code)
        {
            var normalized = NormalizeInviteCode(code);
            var home = await _uow.HomeRepository.GetHomeByInviteCodeAsync(normalized, trackChanges: false);
            return home; // return null if not found
        }

        public async Task<Home> JoinHomeByInviteCodeAsync(string userId, string code)
        {
            var normalized = NormalizeInviteCode(code);

            // Ensure user exists (optional but recommended)
            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            // Fetch home (no need to track changes; we aren't modifying the Home entity)
            var home = await _uow.HomeRepository.GetHomeByInviteCodeAsync(normalized, trackChanges: false);
            if (home is null)
                throw new InvalidOperationException($"Invite code '{normalized}' is invalid.");

            // Idempotent: if already a member, just return the home
            if (await _uow.HomeMemberRepository.IsUserInHomeAsync(userId, home.Id))
            {
                _logger.LogInformation("User {UserId} attempted to join home {HomeId} again via code {Code}. No-op.", userId, home.Id, normalized);
                return home;
            }

            // Add as Member
            var member = new HomeMember
            {
                HomeId = home.Id,
                UserId = userId,
                Role = Role.Member
            };
            await _uow.HomeMemberRepository.AddMemberAsync(member);

            await _uow.SaveAsync();

            _logger.LogInformation("User {UserId} joined home {HomeId} via code {Code}", userId, home.Id, normalized);
            return home;
        }

        // -------- Private helpers --------

        private static string NormalizeInviteCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Invite code is required.", nameof(code));

            return code.Trim().ToUpperInvariant();
        }

        private async Task<string> GenerateUniqueInviteCodeAsync()
        {
            // Ensure you also have a unique index on Homes.InviteCode at the EF/model level
            string code;
            do
            {
                code = InviteCodeGenerator.GenerateCode(length: 6);
            }
            while (await _uow.HomeRepository.GetHomeByInviteCodeAsync(code, trackChanges: false) is not null);

            return code;
        }
    }
}