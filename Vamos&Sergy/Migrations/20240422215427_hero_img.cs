using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Migrations
{
    public partial class hero_img : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Heroes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Heroes");
        }
    }
}
