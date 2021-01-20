using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineRefrigerator.Migrations.Ingredients
{
    public partial class ServingValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServingValue",
                table: "Servings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServingValue",
                table: "Servings");
        }
    }
}
