using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jewelery.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubCategory_id",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DetermineTheSize_Id",
                table: "Atributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Atributes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryDTO",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CategoryVMDTO",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DetermineTheSizes",
                columns: table => new
                {
                    DetermineTheSize_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetermineTheSizes", x => x.DetermineTheSize_Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoryVMDTO",
                columns: table => new
                {
                    SubCategory_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atributes_DetermineTheSize_Id",
                table: "Atributes",
                column: "DetermineTheSize_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Atributes_DetermineTheSizes_DetermineTheSize_Id",
                table: "Atributes",
                column: "DetermineTheSize_Id",
                principalTable: "DetermineTheSizes",
                principalColumn: "DetermineTheSize_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atributes_DetermineTheSizes_DetermineTheSize_Id",
                table: "Atributes");

            migrationBuilder.DropTable(
                name: "CategoryDTO");

            migrationBuilder.DropTable(
                name: "CategoryVMDTO");

            migrationBuilder.DropTable(
                name: "DetermineTheSizes");

            migrationBuilder.DropTable(
                name: "SubCategoryVMDTO");

            migrationBuilder.DropIndex(
                name: "IX_Atributes_DetermineTheSize_Id",
                table: "Atributes");

            migrationBuilder.DropColumn(
                name: "DetermineTheSize_Id",
                table: "Atributes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Atributes");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategory_id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
