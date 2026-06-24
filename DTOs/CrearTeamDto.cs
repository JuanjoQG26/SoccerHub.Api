using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.DTOs
{
    public class CrearTeamDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
