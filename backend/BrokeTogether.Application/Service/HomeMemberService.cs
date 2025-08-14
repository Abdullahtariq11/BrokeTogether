// BrokeTogether.Application/Service/HomeMemberService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using BrokeTogether.Core.Enums;
using Microsoft.Extensions.Logging;

namespace BrokeTogether.Application.Service
{
    public class HomeMemberService : IHomeMemberService
    {
        private readonly IRepositoryManager _uow;
        private readonly ILogger<HomeMemberService> _logger;

        public HomeMemberService(IRepositoryManager repositoryManager, ILogger<HomeMemberService> logger)
        {
            _uow = repositoryManager;
            _logger = logger;
        }

        public async Task<HomeMember> AddMemberAsync(Guid homeId, string userId)
        {
            _logger.LogInformation("Add member: user {UserId} to home {HomeId}", userId, homeId);

            // Validate home exists (read-only)
            var home = await _uow.HomeRepository.GetByIdAsync(homeId, trackChanges: false);
            if (home is null)
                throw new InvalidOperationException($"Home '{homeId}' was not found.");

            // Validate user exists (read-only)
            var user = await _uow.UserRepository.GetByIdAsync(userId, trackChanges: false);
            if (user is null)
                throw new InvalidOperationException($"User '{userId}' was not found.");

            // Idempotency: already a member?
            if (await _uow.HomeMemberRepository.IsUserInHomeAsync(userId, homeId))
            {
                _logger.LogInformation("User {UserId} is already a member of home {HomeId}", userId, homeId);
                var existing = await _uow.HomeMemberRepository.GetByUserAndHomeAsync(userId, homeId, trackChanges: false);
                return existing!;
            }

            var member = new HomeMember
            {
                HomeId = homeId,
                UserId = userId,
                Role = Role.Member
            };

            await _uow.HomeMemberRepository.AddMemberAsync(member);
            await _uow.SaveAsync();

            _logger.LogInformation("User {UserId} added to home {HomeId}", userId, homeId);
            return member;
        }

        public async Task<IEnumerable<HomeMember>> GetMembersAsync(Guid homeId)
        {
            // Optionally validate home exists; for MVP we can just return empty if none
            var members = await _uow.HomeMemberRepository.GetMembersByHomeIdAsync(homeId, trackChanges: false);
            return members;
        }

        public Task<bool> IsAdminAsync(Guid homeId, string userId)
            => _uow.HomeMemberRepository.IsAdminAsync(userId, homeId);

        public Task<bool> IsUserInHomeAsync(Guid homeId, string userId)
            => _uow.HomeMemberRepository.IsUserInHomeAsync(userId, homeId);

        public async Task RemoveMemberAsync(Guid homeId, string userId, string actingUserId)
        {
            // Allow self-leave; otherwise, require admin
            var selfLeave = string.Equals(userId, actingUserId, StringComparison.Ordinal);
            if (!selfLeave && !await _uow.HomeMemberRepository.IsAdminAsync(actingUserId, homeId))
                throw new UnauthorizedAccessException($"Acting user '{actingUserId}' is not an admin for home '{homeId}'.");

            // Fetch target membership (tracking because we'll delete)
            var member = await _uow.HomeMemberRepository.GetByUserAndHomeAsync(userId, homeId, trackChanges: true);
            if (member is null)
                throw new InvalidOperationException($"User '{userId}' is not a member of home '{homeId}'.");

            // Guard: do not remove the last admin
            if (member.Role == Role.Admin)
            {
                var allMembers = await _uow.HomeMemberRepository.GetMembersByHomeIdAsync(homeId, trackChanges: false);
                var adminCount = allMembers.Count(m => m.Role == Role.Admin);
                if (adminCount <= 1)
                    throw new InvalidOperationException("Cannot remove the last admin from the home.");
            }

            await _uow.HomeMemberRepository.RemoveMemberAsync(member);
            await _uow.SaveAsync();

            _logger.LogInformation("User {UserId} removed from home {HomeId} by {ActingUserId}", userId, homeId, actingUserId);
        }

        public async Task<HomeMember> UpdateMemberRoleAsync(Guid homeId, string userId, string actingUserId, string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole))
                throw new ArgumentException("Role is required.", nameof(newRole));

            if (!await _uow.HomeMemberRepository.IsAdminAsync(actingUserId, homeId))
                throw new UnauthorizedAccessException($"Acting user '{actingUserId}' is not an admin for home '{homeId}'.");

            // Fetch target membership (tracking because we'll update)
            var member = await _uow.HomeMemberRepository.GetByUserAndHomeAsync(userId, homeId, trackChanges: true);
            if (member is null)
                throw new InvalidOperationException($"User '{userId}' is not a member of home '{homeId}'.");

            // Parse role safely (case-insensitive)
            if (!Enum.TryParse<Role>(newRole.Trim(), ignoreCase: true, out var parsedRole))
                throw new ArgumentException($"Invalid role '{newRole}'. Allowed values: {string.Join(", ", Enum.GetNames(typeof(Role)))}", nameof(newRole));

            // If demoting Admin to Member, guard last admin
            if (member.Role == Role.Admin && parsedRole != Role.Admin)
            {
                var allMembers = await _uow.HomeMemberRepository.GetMembersByHomeIdAsync(homeId, trackChanges: false);
                var adminCount = allMembers.Count(m => m.Role == Role.Admin);
                if (adminCount <= 1)
                    throw new InvalidOperationException("Cannot demote the last admin in the home.");
            }

            member.Role = parsedRole;
            await _uow.HomeMemberRepository.UpdateMemberAsync(member);
            await _uow.SaveAsync();

            _logger.LogInformation("User {UserId} role updated to {Role} in home {HomeId} by {ActingUserId}", userId, parsedRole, homeId, actingUserId);
            return member;
        }
    }
}