namespace SoccerHub.Api.DTOs
{
    public class CreatePlayerDto
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Posicion { get; set; } = string.Empty;

        public int Number { get; set; }

        public int TeamId { get; set; }
    }
}
