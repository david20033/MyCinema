using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class UndoLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ScreeningId",
                table: "Ticket",
                column: "ScreeningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Screening_ScreeningId",
                table: "Ticket",
                column: "ScreeningId",
                principalTable: "Screening",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Screening_ScreeningId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_ScreeningId",
                table: "Ticket");
        }
    }
}
