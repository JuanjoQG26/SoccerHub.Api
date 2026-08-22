namespace SoccerHub.Api.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalTeams { get; set; }

        public int TotalPlayers { get; set; }

        public int MatchesPlayed { get; set; }

        public int MatchesSheduled { get; set; }

        public int TotalGoals { get; set; }

        public double AverageGoalsPerMatch { get; set; }

        public string Leader { get; set; } = string.Empty;

        public string TopScoringTeam { get; set; } = string.Empty;
    }
}
