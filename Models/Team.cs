using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.Models
{
    public class Team
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();

        //ESTADISTICAS
        public int Played { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losses { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgaints { get; set; }

        public int Points { get; set; }
    }
}
