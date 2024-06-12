using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWebshopUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebshopUrl",
                table: "SearchResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebshopUrl",
                table: "SearchResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
