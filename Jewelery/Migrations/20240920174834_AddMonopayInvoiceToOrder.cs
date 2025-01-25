using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class AddMonopayInvoiceToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invoiceId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invoiceId",
                table: "Orders");
        }
    }
}
