using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set;  } = string.Empty;
    }
}
