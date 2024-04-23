using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Migrations
{
    public partial class hero_inv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxInvetory",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxInvetory",
                table: "Heroes");
        }
    }
}
