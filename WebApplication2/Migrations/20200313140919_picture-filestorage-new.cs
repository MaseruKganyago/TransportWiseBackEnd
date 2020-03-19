using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class picturefilestoragenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileStorage",
                columns: table => new
                {
                    FileStorageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FileTye = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorage", x => x.FileStorageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FileStorage_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "FileStorage",
                principalColumn: "FileStorageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FileStorage_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FileStorage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");
        }
    }
}
