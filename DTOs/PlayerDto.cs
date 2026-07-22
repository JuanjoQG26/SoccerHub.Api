namespace SoccerHub.Api.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Posicion { get; set; } = string.Empty;

        public int Number { get; set; }
    }
}
