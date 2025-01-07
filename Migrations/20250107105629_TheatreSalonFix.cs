using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class TheatreSalonFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacesColumns",
                table: "TheatreSalon");

            migrationBuilder.RenameColumn(
                name: "PlacesRows",
                table: "TheatreSalon",
                newName: "EmptySeatsCoords");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "TheatreSalon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "TheatreSalon");

            migrationBuilder.RenameColumn(
                name: "EmptySeatsCoords",
                table: "TheatreSalon",
                newName: "PlacesRows");

            migrationBuilder.AddColumn<string>(
                name: "PlacesColumns",
                table: "TheatreSalon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
