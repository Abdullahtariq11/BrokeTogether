using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Auth;
using BrokeTogether.Application.Service;
using BrokeTogether.Application.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokeTogether.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _auth = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await _auth.RegisterAsync(registerUserDto);
            return Ok(result);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _auth.LoginAsync(loginUserDto);
            return Ok(result);
        }
        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest dto)
        {
            var result = await _auth.RefreshAsync(dto);
            return Ok(result);
        }
        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();
            var ok = await _auth.RevokeRefreshTokensAsync(userId!);
            return Ok(new { succeeded = ok });
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> UserDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();
            var userDetails = await _userService.GetUserDetailByIdAsync(userId!);
            return Ok(userDetails);
        }
       /* Implement later [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

        }*/


    }
}