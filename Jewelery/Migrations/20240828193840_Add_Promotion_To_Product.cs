using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class Add_Promotion_To_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Promotion_Price",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isPromotion",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Promotion_Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "isPromotion",
                table: "Products");
        }
    }
}
