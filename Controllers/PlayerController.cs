using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;

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
        public async Task<IActionResult> Create(CrearPlayerDto dto)
        {
            var player = new Player
            {
                Name = dto.Name,
                Age = dto.Age,
                Posicion = dto.Posicion,
                TeamId = dto.TeamId,
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _context.Players
                .Include(p => p.Team).ToListAsync();

            return Ok(players);
        }
    }
}
