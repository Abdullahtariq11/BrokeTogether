using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrokeTogether.Application.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokeTogether.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private IServiceManager _services;
        public HomeController(IServiceManager serviceManager)
        {

            _services = serviceManager;
        }

        private string RequiredUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Missing user id claim.");

        [HttpPost("/create")]
        [Authorize]
        public async Task<IActionResult> CreateHome([FromBody] string name)
        {
            string userId = RequiredUserId();
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();
            var result = await _services.Homes.CreateHomeAsync(userId, name);
            if (result is null)
                return BadRequest("No home exists");

            return CreatedAtAction(nameof(CreateHome),nameof(result));

        }
    }
}