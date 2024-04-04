using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Data.Migrations
{
    public partial class hero_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Exp = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Kast = table.Column<int>(type: "int", nullable: false),
                    HasMount = table.Column<bool>(type: "bit", nullable: false),
                    Str = table.Column<int>(type: "int", nullable: false),
                    Dex = table.Column<int>(type: "int", nullable: false),
                    Inte = table.Column<int>(type: "int", nullable: false),
                    Vit = table.Column<int>(type: "int", nullable: false),
                    Luck = table.Column<double>(type: "float", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_OwnerId",
                table: "Heroes",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
