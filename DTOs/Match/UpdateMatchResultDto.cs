using System.ComponentModel.DataAnnotations;
namespace SoccerHub.Api.DTOs.Match
{
    public class UpdateMatchResultDto
    {
        [Range(0, 50)]
        public int HomeGoals { get; set; }

        [Range(0, 50)]
        public int AwayGoals { get; set; }
    }
}
