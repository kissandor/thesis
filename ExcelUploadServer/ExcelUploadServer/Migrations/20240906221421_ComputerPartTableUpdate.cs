using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    /// <inheritdoc />
    public partial class ComputerPartTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerParts_WebShops_WebshopId",
                table: "ComputerParts");

            migrationBuilder.DropIndex(
                name: "IX_ComputerParts_WebshopId",
                table: "ComputerParts");

            migrationBuilder.DropColumn(
                name: "ComputerPartPrice",
                table: "ComputerParts");

            migrationBuilder.DropColumn(
                name: "WebShopId",
                table: "ComputerParts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ComputerPartPrice",
                table: "ComputerParts",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WebShopId",
                table: "ComputerParts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComputerParts_WebshopId",
                table: "ComputerParts",
                column: "WebShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerParts_WebShops_WebshopId",
                table: "ComputerParts",
                column: "WebShopId",
                principalTable: "WebShops",
                principalColumn: "Id");
        }
    }
}
