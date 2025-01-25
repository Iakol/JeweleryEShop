using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class DetermineSizeAddTitleAndNAme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DetermineTheSizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tittle",
                table: "DetermineTheSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DetermineTheSizes");

            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "DetermineTheSizes");
        }
    }
}
