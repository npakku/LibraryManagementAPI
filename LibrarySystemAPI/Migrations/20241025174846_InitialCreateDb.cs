using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailabilityStatus = table.Column<bool>(type: "bit", nullable: true),
                    Edition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailabilityStatus", "Edition", "Genre", "PublicationDate", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, "Pascal Zury", true, "Tenth Edition", "Science", new DateTime(1945, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Linear Data Structures, Non Linear Data Structures", "Introduction to Data Structures" },
                    { 2, "Eric Zury", true, "Eleventh Edition", "Engineering", new DateTime(1955, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Signals, Controls", "Introduction to Electronics" },
                    { 3, "Pascal Zury", true, "First Edition", "Science", new DateTime(1925, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Objects and Classes in C#", "Introduction to C#" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
