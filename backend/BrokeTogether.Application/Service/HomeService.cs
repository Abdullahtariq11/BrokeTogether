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

            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            // Ensure uniqueness beyond retry by having a unique index on InviteCode in the model.
            var inviteCode = await GenerateUniqueInviteCodeAsync();

            var home = new Home
            {
                Name = name.Trim(),
                InviteCode = inviteCode
            };

            await _uow.HomeRepository.CreateHomeAsync(home);

            // Creator is Admin by default
            var adminMember = new HomeMember
            {
                HomeId = home.Id,
                UserId = userId,
                Role = Role.Admin
            };
            await _uow.HomeMemberRepository.AddMemberAsync(adminMember);

            await _uow.SaveAsync();

            _logger.LogInformation("Home {HomeId} created with invite code {Code}", home.Id, inviteCode);
            return home;
        }

        public async Task<Home?> GetHomeAsync(Guid homeId)
        {
            var home = await _uow.HomeRepository.GetByIdAsync(homeId, trackChanges: false);
            return home;
        }

        public async Task<IEnumerable<Home>> GetHomesForUserAsync(string userId)
        {
            var homes = await _uow.HomeRepository.GetAllHomeForUser(userId, trackChanges: false);
            return homes;
        }

        public async Task<Home?> GetHomeByInviteCodeAsync(string code)
        {
            var normalized = NormalizeInviteCode(code);
            var home = await _uow.HomeRepository.GetHomeByInviteCodeAsync(normalized, trackChanges: false);
            return home;
        }

        public async Task<Home> JoinHomeByInviteCodeAsync(string userId, string code)
        {
            var normalized = NormalizeInviteCode(code);

            // Validate user exists (read-only)
            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            // Fetch home (read-only; we aren't modifying the Home entity itself)
            var home = await _uow.HomeRepository.GetHomeByInviteCodeAsync(normalized, trackChanges: false);
            if (home is null)
                throw new InvalidOperationException($"Invite code '{normalized}' is invalid.");

            // Idempotent: if already a member, no-op
            if (await _uow.HomeMemberRepository.IsUserInHomeAsync(userId, home.Id))
            {
                _logger.LogInformation(
                    "User {UserId} attempted to join home {HomeId} again via code {Code}. No-op.",
                    userId, home.Id, normalized
                );
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

        // ---------- Helpers ----------

        private static string NormalizeInviteCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Invite code is required.", nameof(code));

            return code.Trim().ToUpperInvariant();
        }

        private async Task<string> GenerateUniqueInviteCodeAsync()
        {
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