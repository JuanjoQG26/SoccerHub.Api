using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs.Dashboard;

namespace SoccerHub.Api.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetAsync()
        {
            var totalTeams = await _context.Teams.CountAsync();

            var totalPlayers = await _context.Players.CountAsync();

            var matchesPlayed = await _context.Matches.CountAsync(m => m.Status == Models.MatchStatus.Finished);

            var matchesSheduled = await _context.Matches.CountAsync(m => m.Status == Models.MatchStatus.Schelduled);

            var totalGoals = await _context.Matches.Where(m => m.Status == Models.MatchStatus.Finished)
                .SumAsync(m => m.HomeGoals + m.AwayGoals);

            double averageGoals = matchesPlayed == 0 ? 0 : (double)totalGoals / matchesPlayed;

            var leader = await _context.Teams
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalsFor - t.GoalsAgaints)
                .Select(t => t.Name)
                .FirstOrDefaultAsync() ?? "";

            var topScoringTeam = await _context.Teams
                .OrderByDescending(t => t.GoalsFor)
                .Select(t => t.Name)
                .FirstOrDefaultAsync() ?? "";


            return new DashboardDto
            {
                TotalTeams = totalTeams,
                TotalPlayers = totalPlayers,
                MatchesPlayed = matchesPlayed,
                MatchesSheduled = matchesSheduled,
                TotalGoals = totalGoals,
                AverageGoalsPerMatch = Math.Round(averageGoals, 2),
                Leader = leader,
                TopScoringTeam = topScoringTeam
            };
        }
    }
}
