using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Migrations
{
    public partial class hero_mount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMount",
                table: "Heroes");

            migrationBuilder.AddColumn<int>(
                name: "Mount",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mount",
                table: "Heroes");

            migrationBuilder.AddColumn<bool>(
                name: "HasMount",
                table: "Heroes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
