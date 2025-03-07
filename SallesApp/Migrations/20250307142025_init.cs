using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SallesApp.Context;

#nullable disable

namespace SallesApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCategoryId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ShortDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    LongDescription = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    ImageThumbnailUrl = table.Column<string>(type: "text", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: nameof(ApplicationDbContext.ProductCategories),
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,] {
                        { (short)1, "Hamburguer", "Sanduíches e Lanches" },
                        { (short)2, "Pizza", "Tudo acaba em PIZZA" },
                        { (short)3, "Bebida", "Vitaminas, sucos e refrigerantes" }
              });

            migrationBuilder.InsertData(
                table: nameof(ApplicationDbContext.Products),
                columns: new[] { "Id", "ProductCategoryId", "Name", "Price", "ShortDescription", "LongDescription" },
                values: new object[,] {
                                { 1, (short)1, "MonsterBurgue", 20.99d, "Hamburguer gigantesco", "Para matar fomes monstruosas" },
                                { 2, (short)1, "CheeseBacon", 18.50d, "Clássico com bacon", "Queijo derretido e bacon crocante" },
                                { 3, (short)1, "VeggieBurgue", 16.99d, "Opção vegetariana", "Feito com ingredientes naturais e saudáveis" },
                                { 4, (short)2, "Calabresa", 35.99d, "Tradicional calabresa", "Queijo, molho e calabresa especial" },
                                { 5, (short)2, "4 Queijos", 38.50d, "Delícia para os amantes de queijo", "Combinação irresistível de queijos selecionados" },
                                { 6, (short)2, "Frango com Catupiry", 39.99d, "Clássico do Brasil", "Frango desfiado com catupiry original" },
                                { 7, (short)3, "Suco de Laranja", 8.99d, "Natural e saudável", "Suco feito com laranjas frescas" },
                                { 8, (short)3, "Refrigerante Cola", 6.50d, "Refrescante e gaseificado", "O clássico refrigerante de cola" },
                                { 9, (short)3, "Milkshake de Chocolate", 12.99d, "Cremosidade e sabor", "Feito com sorvete e calda de chocolate premium" }
                             });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
