using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var email = User.FindFirst("email");

            var name = User.Identity?.Name;

            return Ok(new {name, email=email?.Value});
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);

            var email = User.FindFirst(ClaimTypes.Email);

            var role = User.FindFirst(ClaimTypes.Role);

            return Ok(new
            {
                Id = id?.Value,
                Email = email?.Value,
                Role = role?.Value
            });
        }
    }
}
