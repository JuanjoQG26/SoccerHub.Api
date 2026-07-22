using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.Models
{
    public class Player
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Posicion { get; set; } = string.Empty;

        public int Number {  get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; } = null!;
    }
}
