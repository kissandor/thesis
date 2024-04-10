using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchResultTableToDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ComputerPartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputerPartPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    WebshopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_SearchResults_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchResults_WebShops_WebshopId",
                        column: x => x.WebshopId,
                        principalTable: "WebShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_CategoryId",
                table: "SearchResults",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchResults_WebshopId",
                table: "SearchResults",
                column: "WebshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResults");
        }
    }
}
