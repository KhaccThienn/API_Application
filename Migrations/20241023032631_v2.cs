using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Application.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "favourite",
                columns: table => new
                {
                    comic_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favourite", x => new { x.comic_id, x.user_id });
                    table.ForeignKey(
                        name: "FK__comic_act__comic__4E88ABD9",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__comic_act__user__4F7CD00D",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comic_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    episode_id = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__history__3213E83F0636D778", x => x.id);
                    table.ForeignKey(
                        name: "FK__comic_act__comic__4E88ABD1",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__comic_act__episode__4E88ABD4",
                        column: x => x.episode_id,
                        principalTable: "episode",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__comic_act__user__4F7CD10D",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_favourite_user_id",
                table: "favourite",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_comic_id",
                table: "history",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_episode_id",
                table: "history",
                column: "episode_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_user_id",
                table: "history",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favourite");

            migrationBuilder.DropTable(
                name: "history");
        }
    }
}
