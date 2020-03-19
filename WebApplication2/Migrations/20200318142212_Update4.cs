using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }
    }
}
