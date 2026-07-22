using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using SoccerHub.Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SoccerHub.Api.Services.Interfaces;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var response = await _authService.RegisterAsync(dto);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);

            var name = User.FindFirst(ClaimTypes.Name);

            var email = User.FindFirst(ClaimTypes.Email);

            var role = User.FindFirst(ClaimTypes.Role);

            return Ok(new
            {
                Id = id?.Value,
                Name = name?.Value,
                Email = email?.Value,
                Role = role?.Value
            });

            /*return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated,
                AuthenticationType = User.Identity?.AuthenticationType,
                Claims = User.Claims.Select(c => new
                {
                    c.Type,
                    c.Value
                })
            });*/
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Funciona");
        }
    }
}
