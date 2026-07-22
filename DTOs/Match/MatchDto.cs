namespace SoccerHub.Api.DTOs.Match
{
    public class MatchDto
    {
        public int Id { get; set; }

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam {  get; set; } = string.Empty;

        public DateTime MatchDate { get; set; }
        
        public string Stadium { get; set; } = string.Empty;

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public string Status {  get; set; } = string.Empty;
    }
}
