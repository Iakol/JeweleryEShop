using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class ADD_option_Refactor_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Carts_items_Cart_itemItem_id",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Order_details_Order_detail_id",
                table: "Options");

            migrationBuilder.DropTable(
                name: "CategoryDTO");

            migrationBuilder.DropTable(
                name: "CategoryVMDTO");

            migrationBuilder.DropTable(
                name: "SubCategoryVMDTO");

            migrationBuilder.DropIndex(
                name: "IX_Options_Cart_itemItem_id",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_Order_detail_id",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details");

            migrationBuilder.DropColumn(
                name: "Cart_itemItem_id",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Order_detail_id",
                table: "Options");

            migrationBuilder.RenameColumn(
                name: "Session_id",
                table: "Carts",
                newName: "User_id");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Delivery_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cart_item_options",
                columns: table => new
                {
                    Cart_item_option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cart_item_id = table.Column<int>(type: "int", nullable: false),
                    Atribute_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_item_options", x => x.Cart_item_option_id);
                    table.ForeignKey(
                        name: "FK_Cart_item_options_Carts_items_Cart_item_id",
                        column: x => x.Cart_item_id,
                        principalTable: "Carts_items",
                        principalColumn: "Item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_detail_options",
                columns: table => new
                {
                    Order_detail_option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_detail_id = table.Column<int>(type: "int", nullable: false),
                    Atribute_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_detail_options", x => x.Order_detail_option_id);
                    table.ForeignKey(
                        name: "FK_Order_detail_options_Order_details_Order_detail_id",
                        column: x => x.Order_detail_id,
                        principalTable: "Order_details",
                        principalColumn: "Order_detail_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_item_options_Cart_item_id",
                table: "Cart_item_options",
                column: "Cart_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_detail_options_Order_detail_id",
                table: "Order_detail_options",
                column: "Order_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_userId",
                table: "Carts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_userId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "Cart_item_options");

            migrationBuilder.DropTable(
                name: "Order_detail_options");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details");

            migrationBuilder.DropIndex(
                name: "IX_Carts_userId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Delivery_details");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Carts",
                newName: "Session_id");

            migrationBuilder.AddColumn<int>(
                name: "Cart_itemItem_id",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order_detail_id",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryDTO",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CategoryVMDTO",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryVMDTO",
                columns: table => new
                {
                    Image = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategory_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_Cart_itemItem_id",
                table: "Options",
                column: "Cart_itemItem_id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Order_detail_id",
                table: "Options",
                column: "Order_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details",
                column: "Order_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Carts_items_Cart_itemItem_id",
                table: "Options",
                column: "Cart_itemItem_id",
                principalTable: "Carts_items",
                principalColumn: "Item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Order_details_Order_detail_id",
                table: "Options",
                column: "Order_detail_id",
                principalTable: "Order_details",
                principalColumn: "Order_detail_id");
        }
    }
}
