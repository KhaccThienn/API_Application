using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Application.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_Date",
                table: "history");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "history",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "history",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "favourite",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "history");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "history");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "favourite");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Created_Date",
                table: "history",
                type: "date",
                nullable: true);
        }
    }
}
