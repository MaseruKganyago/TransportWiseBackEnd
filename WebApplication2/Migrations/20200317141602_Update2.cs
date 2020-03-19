using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstProject.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileTye",
                table: "FileStorage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileTye",
                table: "FileStorage",
                nullable: true);
        }
    }
}
