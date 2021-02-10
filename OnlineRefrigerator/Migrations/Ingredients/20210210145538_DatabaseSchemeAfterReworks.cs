using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineRefrigerator.Migrations.Ingredients
{
    public partial class DatabaseSchemeAfterReworks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    ServingType = table.Column<int>(nullable: false),
                    ServingQuantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsRecipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipesImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipesSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(nullable: true),
                    StepNumber = table.Column<int>(nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipesSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServingType = table.Column<string>(nullable: true),
                    ServingValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserVotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    RecipeId = table.Column<int>(nullable: false),
                    VoteValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: true),
                    PreparationTime = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true),
                    VoteValue = table.Column<int>(nullable: true),
                    VoteCounts = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_RecipesImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "RecipesImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_RecipesCategories_TypeId",
                        column: x => x.TypeId,
                        principalTable: "RecipesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Fat = table.Column<decimal>(nullable: false),
                    Carbs = table.Column<decimal>(nullable: false),
                    Protein = table.Column<decimal>(nullable: false),
                    Energy = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    ServingId = table.Column<int>(nullable: true),
                    ServingValue = table.Column<int>(nullable: true),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientsCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "IngredientsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientsImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "IngredientsImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Servings_ServingId",
                        column: x => x.ServingId,
                        principalTable: "Servings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_CategoryId",
                table: "Ingredients",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ImageId",
                table: "Ingredients",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_ServingId",
                table: "Ingredients",
                column: "ServingId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ImageId",
                table: "Recipes",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_TypeId",
                table: "Recipes",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "IngredientsRecipes");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipesSteps");

            migrationBuilder.DropTable(
                name: "UserVotes");

            migrationBuilder.DropTable(
                name: "IngredientsCategories");

            migrationBuilder.DropTable(
                name: "IngredientsImages");

            migrationBuilder.DropTable(
                name: "Servings");

            migrationBuilder.DropTable(
                name: "RecipesImages");

            migrationBuilder.DropTable(
                name: "RecipesCategories");
        }
    }
}
