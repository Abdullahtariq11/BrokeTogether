using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Auth;
using BrokeTogether.Application.Service.Contracts;
using BrokeTogether.Core.Contracts;
using BrokeTogether.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace BrokeTogether.Application.Service
{
    public class UserService : IUserService
    {

        private readonly IRepositoryManager _uow;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, IRepositoryManager repositoryManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _uow = repositoryManager;
            _logger = logger;


        }
        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSummary?> GetUserDetailByIdAsync(string userId)
        {
            _logger.LogInformation("Getting user details");

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Invalid User");
            }
            var userDto = new UserSummary(userId, user.Email, user.FullName);

            return userDto;
        }

        public Task<IEnumerable<User>> GetUsersByHomeIdAsync(Guid homeId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}