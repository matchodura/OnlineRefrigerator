using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineRefrigerator.Migrations.Ingredients
{
    public partial class IngredientsCategoriesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Ingredient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Ingredient");
        }
    }
}
