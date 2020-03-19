using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class withUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dislikes");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "Email");

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

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LikesCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                });
        }
    }
}
