using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Auth;
using BrokeTogether.Core.Entities;

namespace BrokeTogether.Application.Service.Contracts
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterUserDto dto);
        Task<AuthResult> LoginAsync(LoginUserDto dto);
        Task<AuthResult> RefreshAsync(RefreshTokenRequest dto);
        Task<bool> RevokeRefreshTokensAsync(string userId); // optional but useful for logout/all-devices
    

    }
}