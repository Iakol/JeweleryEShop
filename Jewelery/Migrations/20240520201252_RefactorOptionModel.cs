using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class RefactorOptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.AddColumn<int>(
                name: "Product_id",
                table: "Atributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Atribute_id = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cart_itemItem_id = table.Column<int>(type: "int", nullable: true),
                    Order_detail_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Option_id);
                    table.ForeignKey(
                        name: "FK_Options_Atributes_Atribute_id",
                        column: x => x.Atribute_id,
                        principalTable: "Atributes",
                        principalColumn: "Atribute_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Options_Carts_items_Cart_itemItem_id",
                        column: x => x.Cart_itemItem_id,
                        principalTable: "Carts_items",
                        principalColumn: "Item_id");
                    table.ForeignKey(
                        name: "FK_Options_Order_details_Order_detail_id",
                        column: x => x.Order_detail_id,
                        principalTable: "Order_details",
                        principalColumn: "Order_detail_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atributes_Product_id",
                table: "Atributes",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Atribute_id",
                table: "Options",
                column: "Atribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Cart_itemItem_id",
                table: "Options",
                column: "Cart_itemItem_id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Order_detail_id",
                table: "Options",
                column: "Order_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atributes_Products_Product_id",
                table: "Atributes",
                column: "Product_id",
                principalTable: "Products",
                principalColumn: "Product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atributes_Products_Product_id",
                table: "Atributes");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Atributes_Product_id",
                table: "Atributes");

            migrationBuilder.DropColumn(
                name: "Product_id",
                table: "Atributes");

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Atribute_id = table.Column<int>(type: "int", nullable: false),
                    Cart_itemItem_id = table.Column<int>(type: "int", nullable: true),
                    Order_detail_id = table.Column<int>(type: "int", nullable: true),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => new { x.Product_id, x.Atribute_id });
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Atributes_Atribute_id",
                        column: x => x.Atribute_id,
                        principalTable: "Atributes",
                        principalColumn: "Atribute_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Carts_items_Cart_itemItem_id",
                        column: x => x.Cart_itemItem_id,
                        principalTable: "Carts_items",
                        principalColumn: "Item_id");
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Order_details_Order_detail_id",
                        column: x => x.Order_detail_id,
                        principalTable: "Order_details",
                        principalColumn: "Order_detail_id");
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Atribute_id",
                table: "ProductAttributes",
                column: "Atribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Cart_itemItem_id",
                table: "ProductAttributes",
                column: "Cart_itemItem_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_Order_detail_id",
                table: "ProductAttributes",
                column: "Order_detail_id");
        }
    }
}
