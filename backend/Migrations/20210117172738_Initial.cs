using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizMania.WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EffectBase_Item_ItemId",
                table: "EffectBase");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Characters_CharacterId",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EffectBase",
                table: "EffectBase");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "EffectBase",
                newName: "Effects");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CharacterId",
                table: "Items",
                newName: "IX_Items_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_EffectBase_ItemId",
                table: "Effects",
                newName: "IX_Effects_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Effects",
                table: "Effects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Effects_Items_ItemId",
                table: "Effects",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Characters_CharacterId",
                table: "Items",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effects_Items_ItemId",
                table: "Effects");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Characters_CharacterId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Effects",
                table: "Effects");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "Effects",
                newName: "EffectBase");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CharacterId",
                table: "Item",
                newName: "IX_Item_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Effects_ItemId",
                table: "EffectBase",
                newName: "IX_EffectBase_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EffectBase",
                table: "EffectBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EffectBase_Item_ItemId",
                table: "EffectBase",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Characters_CharacterId",
                table: "Item",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
