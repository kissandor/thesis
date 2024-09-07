using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebshopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebshopURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComputerParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerPartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputerPartPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    WebshopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerParts_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerParts_WebShops_WebshopId",
                        column: x => x.WebshopId,
                        principalTable: "WebShops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerParts_CategoryId",
                table: "ComputerParts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerParts_WebshopId",
                table: "ComputerParts",
                column: "WebShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerParts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "WebShops");
        }
    }
}
