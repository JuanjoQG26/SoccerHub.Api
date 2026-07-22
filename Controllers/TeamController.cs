using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Models;
using SoccerHub.Api.Services;
using SoccerHub.Api.Services.Interfaces;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : BaseController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CrearTeamDto dto)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var team = await _teamService.CreateAsync(dto, userId);

            return Ok(team);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyTeams()
        {
            //var id =int.Parse( User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var teams = await _teamService.GetMyTeamsAsync(CurrentUserId);

            return Ok(teams);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var deleted = await _teamService.DeleteAsync(id, CurrentUserId);

            if (!deleted)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Team not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Team deleted successfully"
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var team = await _teamService.GetByIdAsync(id, CurrentUserId);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTeamDto dto)
        {
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var updated = await _teamService.UpdateAsync(id, dto, CurrentUserId);

            if (!updated)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Team not found"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Team updated successfully"
            }); ;
        }
    }
}
