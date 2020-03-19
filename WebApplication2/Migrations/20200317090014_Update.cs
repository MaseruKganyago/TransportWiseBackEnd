using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FileStorage_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Photo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                nullable: true);

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
    }
}
