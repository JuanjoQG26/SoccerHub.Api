using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Api.DTOs.Match
{
    public class CreateMatchDto
    {

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public DateTime MatchDate  { get; set; }

        [Required]
        [MaxLength(100)]
        public string Stadium {  get; set; } = string.Empty;
    }
}
