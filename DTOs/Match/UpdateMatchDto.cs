using SoccerHub.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.DTOs.Match
{
    public class UpdateMatchDto
    {
        [Required]
        public DateTime Matchdate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Stadium {  get; set; } = string.Empty;

        public MatchStatus Status { get; set; }
    }
}
