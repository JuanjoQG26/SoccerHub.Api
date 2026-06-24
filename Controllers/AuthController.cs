using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using SoccerHub.Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var exitsUser = await _context.Users.AnyAsync(x => x.Email == dto.Email);

            if (exitsUser)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Email ready exists"
                });
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = UserRole.User
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            var token = _tokenService.GenerarToken(user);

            return Ok(new AuthResponseDto { Token = token});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _tokenService.GenerarToken(user);

            return Ok(new AuthResponseDto { Token = token});
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
