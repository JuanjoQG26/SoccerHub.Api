using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Players");
        }
    }
}
