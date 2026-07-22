namespace SoccerHub.Api.DTOs
{
    public class TeamDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<PlayerSummaryDto> Players { get; set; } = new();
    }
}
