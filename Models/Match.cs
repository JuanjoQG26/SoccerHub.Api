namespace SoccerHub.Api.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; } = null!;

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; } = null!;

        public DateTime Matchdate { get; set; }

        public string Stadium { get; set; } = string.Empty;

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public MatchStatus Status { get; set; } = MatchStatus.Schelduled;
    }

    public enum MatchStatus
    {
        Schelduled,
        InProgress,
        Finished,
        Cancelled
    }
}
