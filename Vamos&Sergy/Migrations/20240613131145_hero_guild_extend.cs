using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Migrations
{
    public partial class hero_guild_extend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuildId",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuildName",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "GuildName",
                table: "Heroes");
        }
    }
}
