using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Application.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__actor__3213E83F4FDCEA32", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comic",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    poster = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    release_year = table.Column<int>(type: "int", nullable: true),
                    view = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<double>(type: "float", nullable: true),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true),
                    published_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comic__3213E83F429B0A55", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "director",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__director__3213E83F08CA054B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__genre__3213E83F0636D778", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user__3213E83F98E23F8E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comic_actor",
                columns: table => new
                {
                    comic_id = table.Column<int>(type: "int", nullable: false),
                    actor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comic_actor", x => new { x.comic_id, x.actor_id });
                    table.ForeignKey(
                        name: "FK__comic_act__actor__4F7CD00D",
                        column: x => x.actor_id,
                        principalTable: "actor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__comic_act__comic__4E88ABD4",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "episode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true),
                    published_at = table.Column<DateOnly>(type: "date", nullable: true),
                    comic_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__episode__3213E83F660786D9", x => x.id);
                    table.ForeignKey(
                        name: "FK__episode__comic_i__4AB81AF0",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comic_director",
                columns: table => new
                {
                    comic_id = table.Column<int>(type: "int", nullable: false),
                    director_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comic_director", x => new { x.comic_id, x.director_id });
                    table.ForeignKey(
                        name: "FK__comic_dir__comic__5070F446",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__comic_dir__direc__5165187F",
                        column: x => x.director_id,
                        principalTable: "director",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comic_genre",
                columns: table => new
                {
                    comic_id = table.Column<int>(type: "int", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comic_genre", x => new { x.comic_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK__comic_gen__comic__52593CB8",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__comic_gen__genre__534D60F1",
                        column: x => x.genre_id,
                        principalTable: "genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    comic_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__review__3213E83FD8773093", x => x.id);
                    table.ForeignKey(
                        name: "FK__review__comic_id__4CA06362",
                        column: x => x.comic_id,
                        principalTable: "comic",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__review__user_id__4BAC3F29",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_at = table.Column<DateOnly>(type: "date", nullable: true),
                    episode_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__images__3213E83F7E5CB749", x => x.id);
                    table.ForeignKey(
                        name: "FK__images__episode___4D94879B",
                        column: x => x.episode_id,
                        principalTable: "episode",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comic_actor_actor_id",
                table: "comic_actor",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_comic_director_director_id",
                table: "comic_director",
                column: "director_id");

            migrationBuilder.CreateIndex(
                name: "IX_comic_genre_genre_id",
                table: "comic_genre",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_episode_comic_id",
                table: "episode",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_episode_id",
                table: "images",
                column: "episode_id");

            migrationBuilder.CreateIndex(
                name: "IX_review_comic_id",
                table: "review",
                column: "comic_id");

            migrationBuilder.CreateIndex(
                name: "IX_review_user_id",
                table: "review",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comic_actor");

            migrationBuilder.DropTable(
                name: "comic_director");

            migrationBuilder.DropTable(
                name: "comic_genre");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "actor");

            migrationBuilder.DropTable(
                name: "director");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "episode");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "comic");
        }
    }
}
