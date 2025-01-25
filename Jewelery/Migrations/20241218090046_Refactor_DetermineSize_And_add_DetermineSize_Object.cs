using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_DetermineSize_And_add_DetermineSize_Object : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "DetermineTheSizes");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "DetermineTheSizes");

            migrationBuilder.CreateTable(
                name: "DetermineTheSizeObjects",
                columns: table => new
                {
                    DetermineTheSizeObject_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetermineTheSize_Id = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetermineTheSizeObjects", x => x.DetermineTheSizeObject_Id);
                    table.ForeignKey(
                        name: "FK_DetermineTheSizeObjects_DetermineTheSizes_DetermineTheSize_Id",
                        column: x => x.DetermineTheSize_Id,
                        principalTable: "DetermineTheSizes",
                        principalColumn: "DetermineTheSize_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetermineTheSizeObjects_DetermineTheSize_Id",
                table: "DetermineTheSizeObjects",
                column: "DetermineTheSize_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetermineTheSizeObjects");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "DetermineTheSizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "DetermineTheSizes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
