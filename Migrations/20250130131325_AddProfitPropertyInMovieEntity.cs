using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddProfitPropertyInMovieEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Profit",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Movie");
        }
    }
}
