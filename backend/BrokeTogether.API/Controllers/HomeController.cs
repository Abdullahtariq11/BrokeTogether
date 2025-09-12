using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BrokeTogether.Application.DTOs.Home;
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

        private string RequireUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Missing user id claim.");

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var userId = RequireUserId();
            var home = await _services.Homes.CreateHomeAsync(userId, name);

            var dto = new HomeDto(home.Id, home.Name, home.InviteCode);

            // Return 201 + Location header pointing to GetById
            return CreatedAtAction(nameof(GetById), new { homeId = home.Id }, dto);
        }

        [HttpGet("{homeId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid homeId)
        {
            var home = await _services.Homes.GetHomeAsync(homeId);
            if (home == null) return NotFound();
            return Ok(new HomeDto(home.Id, home.Name, home.InviteCode));
        }
    }
}