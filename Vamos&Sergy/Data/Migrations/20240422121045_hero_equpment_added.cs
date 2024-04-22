using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Data.Migrations
{
    public partial class hero_equpment_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BeltId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyArmorId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BootsId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlovesId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HelmetId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiscId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NecklaceId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RingId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShieldId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeaponId",
                table: "Heroes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Stats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinDamage = table.Column<int>(type: "int", nullable: true),
                    MaxDamage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_BeltId",
                table: "Heroes",
                column: "BeltId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_BodyArmorId",
                table: "Heroes",
                column: "BodyArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_BootsId",
                table: "Heroes",
                column: "BootsId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_GlovesId",
                table: "Heroes",
                column: "GlovesId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_HelmetId",
                table: "Heroes",
                column: "HelmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_MiscId",
                table: "Heroes",
                column: "MiscId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_NecklaceId",
                table: "Heroes",
                column: "NecklaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_RingId",
                table: "Heroes",
                column: "RingId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_ShieldId",
                table: "Heroes",
                column: "ShieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_WeaponId",
                table: "Heroes",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_BeltId",
                table: "Heroes",
                column: "BeltId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_BodyArmorId",
                table: "Heroes",
                column: "BodyArmorId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_BootsId",
                table: "Heroes",
                column: "BootsId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_GlovesId",
                table: "Heroes",
                column: "GlovesId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_HelmetId",
                table: "Heroes",
                column: "HelmetId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_MiscId",
                table: "Heroes",
                column: "MiscId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_NecklaceId",
                table: "Heroes",
                column: "NecklaceId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_RingId",
                table: "Heroes",
                column: "RingId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_ShieldId",
                table: "Heroes",
                column: "ShieldId",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Item_WeaponId",
                table: "Heroes",
                column: "WeaponId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_BeltId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_BodyArmorId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_BootsId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_GlovesId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_HelmetId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_MiscId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_NecklaceId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_RingId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_ShieldId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Item_WeaponId",
                table: "Heroes");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_BeltId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_BodyArmorId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_BootsId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_GlovesId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_HelmetId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_MiscId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_NecklaceId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_RingId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_ShieldId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_WeaponId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "BeltId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "BodyArmorId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "BootsId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "GlovesId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "HelmetId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "MiscId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "NecklaceId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "RingId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "ShieldId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Heroes");
        }
    }
}
