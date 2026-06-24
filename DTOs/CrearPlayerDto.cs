using System.ComponentModel.DataAnnotations;


namespace SoccerHub.Api.DTOs
{
    public class CrearPlayerDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string Posicion { get; set; } = string.Empty;

        public int TeamId { get; set; }
    }
}
