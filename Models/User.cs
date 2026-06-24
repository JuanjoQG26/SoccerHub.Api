using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty; 
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
