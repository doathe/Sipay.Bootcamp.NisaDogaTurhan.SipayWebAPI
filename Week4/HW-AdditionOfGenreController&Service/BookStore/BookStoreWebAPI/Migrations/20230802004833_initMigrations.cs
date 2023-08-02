using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BookStore");

            migrationBuilder.CreateTable(
                name: "Genre",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "BookStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PageCount = table.Column<int>(type: "integer", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "BookStore",
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "BookStore",
                table: "Genre",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Personal Growth" },
                    { 2, true, "Science Fiction" },
                    { 3, true, "Romance" }
                });

            migrationBuilder.InsertData(
                schema: "BookStore",
                table: "Book",
                columns: new[] { "Id", "GenreId", "PageCount", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, 200, new DateTime(2001, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Lean Startup" },
                    { 2, 2, 250, new DateTime(2010, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Herland" },
                    { 3, 3, 540, new DateTime(2001, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Dune" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                schema: "BookStore",
                table: "Book",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Id",
                schema: "BookStore",
                table: "Book",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Id",
                schema: "BookStore",
                table: "Genre",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book",
                schema: "BookStore");

            migrationBuilder.DropTable(
                name: "Genre",
                schema: "BookStore");
        }
    }
}
