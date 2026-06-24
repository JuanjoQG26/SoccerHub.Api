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
    }
}
