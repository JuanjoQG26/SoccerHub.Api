using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SoccerHub.Api.DTOs
{
    public class StandingDto
    {
        public int Position { get; set; }

        public int TeamId { get; set; }

        public string Team {  get; set; } = string.Empty;

        public int Played { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalDifference {  get; set; }

        public int Points { get; set; }
    }
}
