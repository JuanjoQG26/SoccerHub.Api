namespace SoccerHub.Api.DTOs
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int PlayersCount { get; set; }
    }
}
