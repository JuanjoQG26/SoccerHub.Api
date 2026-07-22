using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.Services.Interfaces;
using SoccerHub.Api.Models;
using SoccerHub.Api.DTOs.Common;

namespace SoccerHub.Api.Services
{
    public class TeamService:ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeamDto> CreateAsync(CrearTeamDto dto, int userId)
        {
            var team = new Team
            {
                Name = dto.Name,
                UserId = userId,
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name
            };
        }

        public async Task<List<TeamDto>> GetMyTeamsAsync(int userId)
        {
            return await _context.Teams.AsNoTracking()
                .Where(t => t.UserId == userId)
                .Select(t => new TeamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToListAsync();
        }

        public async Task<TeamDetailsDto> GetByIdAsync(int teamId, int userId)
        {
            var team = await _context.Teams
                .AsNoTracking()
                .Where(t => t.Id == teamId && t.UserId == userId)
                .Select(t => new TeamDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,

                    Players = t.Players
                        .Select(p => new PlayerSummaryDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Number = p.Number
                        }).ToList()
                }).FirstOrDefaultAsync();
            if (team == null)
            {
                return null;
            }

            return team;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTeamDto dto, int userId)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (team == null)
            {
                return false;
            }

            team.Name = dto.Name;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (team == null)
            {
                return false;
            }

            _context.Teams?.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }

        /*public async Task<PagedResponseDto<TeamDto>> GetPagedAsync(PaginationDto pagination)
        {
            
        }*/
    }
}
