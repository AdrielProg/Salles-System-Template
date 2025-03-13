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
                columns: new[] { "Id", "ProductCategoryId", "Name", "Price", "ShortDescription", "LongDescription", "ImageUrl" },
                values: new object[,] {
                                { 1, (short)1, "MonsterBurgue", 20.99d, "Hamburguer gigantesco", "Para matar fomes monstruosas", "https://img.freepik.com/premium-photo/hamburger-with-cheese-lettuce-cutting-board_917118-340.jpg" },
                                { 2, (short)1, "CheeseBacon", 18.50d, "Clássico com bacon", "Queijo derretido e bacon crocante", "https://img.freepik.com/premium-photo/fresh-delicious-cheeseburger-fries-are-black-dark-background_6724-4248.jpg" },
                                { 3, (short)1, "VeggieBurgue", 16.99d, "Opção vegetariana", "Feito com ingredientes naturais e saudáveis", "https://s2.glbimg.com/jqfaCA6V4Yb2xgU1JzPD200Kaxk=/smart/e.glbimg.com/og/ed/f/original/2018/07/20/matilda_vegano_wellington_nemeth_1.jpg" },
                                { 4, (short)2, "Calabresa", 35.99d, "Tradicional calabresa", "Queijo, molho e calabresa especial", "https://vovozitareceitas.com/wp-content/uploads/2020/09/pizza-calabresa.jpg" },
                                { 5, (short)2, "4 Queijos", 38.50d, "Delícia para os amantes de queijo", "Combinação irresistível de queijos selecionados", "https://2.bp.blogspot.com/-xbyp3-N-RKI/UqYP85GPmNI/AAAAAAAAANQ/ln99qi0bmLI/s1600/pizza+de+frango.jpg" },
                                { 6, (short)2, "Frango com Catupiry", 39.99d, "Clássico do Brasil", "Frango desfiado com catupiry original", "https://www.sabornamesa.com.br/media/k2/items/cache/ada34cd2101afafaba465aad112ee3c1_M.jpg"},
                                { 7, (short)3, "Suco de Laranja", 8.99d, "Natural e saudável", "Suco feito com laranjas frescas", "https://thumbs.dreamstime.com/b/orange-juice-12008430.jpg" },
                                { 8, (short)3, "Refrigerante Cola", 6.50d, "Refrescante e gaseificado", "O clássico refrigerante de cola", "https://blogdapublicidade.com/wp-content/uploads/2024/04/historia-logotipo-coca-cola-1000x600.jpg" },
                                { 9, (short)3, "Milkshake de Chocolate", 12.99d, "Cremosidade e sabor", "Feito com sorvete e calda de chocolate premium","https://64.media.tumblr.com/944d41bd9e770bdef0596dcd60c17888/tumblr_o4dx9oixro1sn5m44o2_1280.jpg" }
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
