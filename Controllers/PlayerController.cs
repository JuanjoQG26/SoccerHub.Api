using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == dto.TeamId);

            if (team == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Equipo no encontrado"
                });
            }

            if (team.UserId != userId)
            {
                return Forbid();
            }

            var player = new Player
            {
                Name = dto.Name,
                Age = dto.Age,
                Posicion = dto.Posicion,
                Number = dto.Number,
                TeamId = dto.TeamId,
            };

            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            return Ok(new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                Posicion = player.Posicion,
                Number = player.Number
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _context.Players
                .Include(p => p.Team).ToListAsync();

            return Ok(players);
        }

        [Authorize]
        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Equipo no encontrado"
                });
            }

            if (team.UserId != userId)
            {
                return Forbid();
            }

            var players = await _context.Players
                .Where(p => p.TeamId == teamId).Select(p => new PlayerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Posicion = p.Posicion,
                    Number = p.Number
                }).ToListAsync();

            return Ok(players);
        }
    }
}
