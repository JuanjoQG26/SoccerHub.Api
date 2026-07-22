using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using SoccerHub.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SoccerHub.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }


        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = _tokenService.GenerarToken(user);

            return new AuthResponseDto { Token = token };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDTO dto)
        {
            var exitsUser = await _context.Users.AnyAsync(x => x.Email == dto.Email);

            if (exitsUser)
            {
                throw new Exception("Email already exists");
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

            return new AuthResponseDto { Token = token };
        }
    }
}
