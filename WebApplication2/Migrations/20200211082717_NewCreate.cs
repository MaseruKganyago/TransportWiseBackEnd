using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class NewCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Author_AuthorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticlesId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBack_Author_AuthorId",
                table: "FeedBack");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBack_User_UserId",
                table: "FeedBack");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Articles_ArticlesId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_TipsForEveryOne_User_UserId",
                table: "TipsForEveryOne");

            migrationBuilder.DropIndex(
                name: "IX_TipsForEveryOne_UserId",
                table: "TipsForEveryOne");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ArticlesId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_FeedBack_AuthorId",
                table: "FeedBack");

            migrationBuilder.DropIndex(
                name: "IX_FeedBack_UserId",
                table: "FeedBack");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticlesId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticlesId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ArticlesId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "FuelWise",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelWise", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelWise");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticlesId",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ArticlesId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipsForEveryOne_UserId",
                table: "TipsForEveryOne",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ArticlesId",
                table: "Likes",
                column: "ArticlesId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBack_AuthorId",
                table: "FeedBack",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBack_UserId",
                table: "FeedBack",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticlesId",
                table: "Comments",
                column: "ArticlesId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Author_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticlesId",
                table: "Comments",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_User_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBack_Author_AuthorId",
                table: "FeedBack",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBack_User_UserId",
                table: "FeedBack",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Articles_ArticlesId",
                table: "Likes",
                column: "ArticlesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_User_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TipsForEveryOne_User_UserId",
                table: "TipsForEveryOne",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
