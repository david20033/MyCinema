using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketSoldCountPropertyInMovieEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketSoldCount",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketSoldCount",
                table: "Movie");
        }
    }
}
