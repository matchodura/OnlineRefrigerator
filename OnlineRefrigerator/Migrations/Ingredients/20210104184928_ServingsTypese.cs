using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineRefrigerator.Migrations.Ingredients
{
    public partial class ServingsTypese : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServingId",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServingValue",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServingType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ServingId",
                table: "Ingredients",
                column: "ServingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Servings_ServingId",
                table: "Ingredients",
                column: "ServingId",
                principalTable: "Servings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Servings_ServingId",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "Servings");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_ServingId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ServingId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ServingValue",
                table: "Ingredients");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
