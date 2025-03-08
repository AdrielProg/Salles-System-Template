using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SallesApp.Migrations
{
    /// <inheritdoc />
    public partial class addCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemShoppingCarts_ShoppingCartId",
                table: "ItemShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ItemShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ItemShoppingCarts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ItemShoppingCarts_ShoppingCartId",
                table: "ItemShoppingCarts");
        }
    }
}
