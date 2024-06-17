using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Migrations
{
    public partial class hero_questImproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "QuestStarted",
                table: "Heroes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedQuest",
                table: "Heroes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestStarted",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SelectedQuest",
                table: "Heroes");
        }
    }
}
