using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    /// <inheritdoc />
    public partial class TableRefact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchResults_Categories_CategoryId",
                table: "SearchResults");

            migrationBuilder.DropColumn(
                name: "ComputerPartName",
                table: "SearchResults");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "SearchResults",
                newName: "ComputerPartId");

            migrationBuilder.RenameIndex(
                name: "IX_SearchResults_CategoryId",
                table: "SearchResults",
                newName: "IX_SearchResults_ComputerPartId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SearchResults",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchResults",
                table: "SearchResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchResults_ComputerParts_ComputerPartId",
                table: "SearchResults",
                column: "ComputerPartId",
                principalTable: "ComputerParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchResults_ComputerParts_ComputerPartId",
                table: "SearchResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchResults",
                table: "SearchResults");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SearchResults");

            migrationBuilder.RenameColumn(
                name: "ComputerPartId",
                table: "SearchResults",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SearchResults_ComputerPartId",
                table: "SearchResults",
                newName: "IX_SearchResults_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "ComputerPartName",
                table: "SearchResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchResults_Categories_CategoryId",
                table: "SearchResults",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
