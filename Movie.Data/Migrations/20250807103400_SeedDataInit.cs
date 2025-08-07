using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movie.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Action");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Drama" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Duration", "GenreId", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, 120, 1, 2020, "Fast & Curious" },
                    { 2, 95, 2, 2019, "Laugh Factory" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "MovieId", "Rating", "ReviewerName" },
                values: new object[,]
                {
                    { 1, "Loved it", 1, 5, "Alice" },
                    { 2, "Funny but shallow", 2, 3, "Bob" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "BirthYear", "Name" },
                values: new object[,]
                {
                    { 1, 1956, "Tom Hanks" },
                    { 2, 1949, "Meryl Streep" }
                });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Drama");
        }
    }
}
