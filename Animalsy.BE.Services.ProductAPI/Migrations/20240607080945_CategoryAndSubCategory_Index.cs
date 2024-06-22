using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animalsy.BE.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAndSubCategory_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryAndSubCategory",
                table: "Products",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryAndSubCategory",
                table: "Products",
                column: "CategoryAndSubCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryAndSubCategory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryAndSubCategory",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
