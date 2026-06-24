using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using SoccerHub.Api.Services;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : BaseController
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CrearTeamDto dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            int id = Convert.ToInt32(userId.Value);

            
            var team = new Team
            {
                Name = dto.Name,
                UserId = id
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            var teamDto = new TeamDto
            {
                Id = team.Id,
                Name = team.Name
            };

            /*return Ok(
                new ApiResponse<TeamDto>
                {
                    Success = true,
                    Message = "Equipo creado",
                    Data = teamDto
                }
                );*/
            return Success(
                teamDto,
                "Equipo creado");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyTeams()
        {
            var id =int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var teams = await _context.Teams
                .Where(t => t.UserId == id)
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name =t.Name,
                    CreatedAt = t.CreatedAt,
                    PlayersCount = t.Players.Count()
                })
                .ToListAsync();

            var teamsDtos = teams.Select(t => new TeamDto
            {
                Id =t.Id,
                Name = t.Name,
            });

            return Ok(
                new ApiResponse<IEnumerable<TeamDto>>
                {
                    Success = true,
                    Message = "Equipos encontrados",
                    Data = teamsDtos
                }
                );
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
