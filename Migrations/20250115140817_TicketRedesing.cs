using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class TicketRedesing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOwnership_AspNetUsers_UserId",
                table: "TicketOwnership");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOwnership_Ticket_TicketId",
                table: "TicketOwnership");

            migrationBuilder.DropIndex(
                name: "IX_TicketOwnership_TicketId",
                table: "TicketOwnership");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "ShowDateTime",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TicketOwnership",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "TicketOwnership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SeatsCoords",
                table: "TicketOwnership",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TheatreSalonId",
                table: "TicketOwnership",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketOwnershipId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketOwnership_MovieId",
                table: "TicketOwnership",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOwnership_TheatreSalonId",
                table: "TicketOwnership",
                column: "TheatreSalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketOwnershipId",
                table: "Ticket",
                column: "TicketOwnershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TicketOwnership_TicketOwnershipId",
                table: "Ticket",
                column: "TicketOwnershipId",
                principalTable: "TicketOwnership",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOwnership_AspNetUsers_UserId",
                table: "TicketOwnership",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOwnership_Movie_MovieId",
                table: "TicketOwnership",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOwnership_TheatreSalon_TheatreSalonId",
                table: "TicketOwnership",
                column: "TheatreSalonId",
                principalTable: "TheatreSalon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TicketOwnership_TicketOwnershipId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOwnership_AspNetUsers_UserId",
                table: "TicketOwnership");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOwnership_Movie_MovieId",
                table: "TicketOwnership");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketOwnership_TheatreSalon_TheatreSalonId",
                table: "TicketOwnership");

            migrationBuilder.DropIndex(
                name: "IX_TicketOwnership_MovieId",
                table: "TicketOwnership");

            migrationBuilder.DropIndex(
                name: "IX_TicketOwnership_TheatreSalonId",
                table: "TicketOwnership");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_TicketOwnershipId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "TicketOwnership");

            migrationBuilder.DropColumn(
                name: "SeatsCoords",
                table: "TicketOwnership");

            migrationBuilder.DropColumn(
                name: "TheatreSalonId",
                table: "TicketOwnership");

            migrationBuilder.DropColumn(
                name: "TicketOwnershipId",
                table: "Ticket");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TicketOwnership",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "Ticket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatNumber",
                table: "Ticket",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowDateTime",
                table: "Ticket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TicketOwnership_TicketId",
                table: "TicketOwnership",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOwnership_AspNetUsers_UserId",
                table: "TicketOwnership",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketOwnership_Ticket_TicketId",
                table: "TicketOwnership",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
