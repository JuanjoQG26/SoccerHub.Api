using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Services
{
    public class StandingService
    {
        private readonly AppDbContext _context;

        public StandingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StandingDto>> GetAsync()
        {
            var teams = await _context.Teams
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalsFor - t.GoalsAgaints)
                .ThenByDescending(t => t.GoalsFor)
                .ToListAsync();

            var standings = teams
                .Select((team, index) => new StandingDto
                {
                    Position = index + 1,
                    TeamId = team.Id,
                    Team = team.Name,
                    Played = team.Played,
                    Wins = team.Wins,
                    Draws = team.Draws,
                    Losses = team.Losses,
                    GoalsFor = team.GoalsFor,
                    GoalsAgainst = team.GoalsAgaints,
                    GoalDifference = team.GoalsFor - team.GoalsAgaints,
                    Points = team.Points
                }).ToList();

            return standings;
        }
    }
}
