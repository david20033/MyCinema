using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCinema.Migrations
{
    /// <inheritdoc />
    public partial class DataModelRedesing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MovieActors",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "OriginalTitle",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "PremierDate",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Subtitles",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "DurationInMinutes",
                table: "Movie",
                newName: "moviedb_id");

            migrationBuilder.AddColumn<bool>(
                name: "Adult",
                table: "Movie",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Backdrop_path",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Homapage",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imdb_id",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Original_languageId",
                table: "Movie",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Original_title",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Popularity",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Poster_path",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Release_date",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Revenue",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Runtime",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Movie",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tagline",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Vote_avarage",
                table: "Movie",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Vote_count",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "Language",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieId",
                table: "MovieGenre",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Original_languageId",
                table: "Movie",
                column: "Original_languageId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_MovieId",
                table: "Language",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Movie_MovieId",
                table: "Language",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Language_Original_languageId",
                table: "Movie",
                column: "Original_languageId",
                principalTable: "Language",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Language_Movie_MovieId",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Language_Original_languageId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_MovieId",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_Movie_Original_languageId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_MovieId",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "Adult",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Backdrop_path",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Homapage",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Imdb_id",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Original_languageId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Original_title",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Poster_path",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Release_date",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Tagline",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Vote_avarage",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Vote_count",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Language");

            migrationBuilder.RenameColumn(
                name: "moviedb_id",
                table: "Movie",
                newName: "DurationInMinutes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MovieActors",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Movie",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginalTitle",
                table: "Movie",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PremierDate",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Subtitles",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                columns: new[] { "MovieId", "GenreId" });
        }
    }
}
