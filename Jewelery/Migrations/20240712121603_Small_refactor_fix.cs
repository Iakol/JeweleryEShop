using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class Small_refactor_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Carts_items");

            migrationBuilder.DropColumn(
                name: "Phone_number",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Delivery_details_id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Atribute_Description",
                table: "Cart_item_options",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details",
                column: "Order_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details");

            migrationBuilder.DropColumn(
                name: "Delivery_details_id",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Carts_items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Atribute_Description",
                table: "Cart_item_options",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Phone_number",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_details_Order_id",
                table: "Delivery_details",
                column: "Order_id");
        }
    }
}
