using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vamos_Sergy.Data.Migrations
{
    public partial class itemrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.AddColumn<int>(
                name: "RaceEnum",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_BeltId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_BodyArmorId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_BootsId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_GlovesId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_HelmetId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_MiscId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_NecklaceId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_RingId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_ShieldId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Items_WeaponId",
                table: "Heroes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RaceEnum",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

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
    }
}
