using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class withLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FeedBack");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Likes");

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Likes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dislikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DislikesCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislikes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dislikes");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Likes");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Likes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Likes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedBack",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Messages = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBack", x => x.Id);
                });
        }
    }
}
