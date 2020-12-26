using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Data
{
    public class RecipesContext : DbContext
    {

        public RecipesContext(DbContextOptions<RecipesContext> options) : base(options) { }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<RecipesSteps> RecipesSteps { get; set; }
        public DbSet<RecipesImages> RecipesImages { get; set; }

        public DbSet<RecipesCategories> RecipesCategories { get; set; }

        //TODO: zrozumieć to i przeczytać dokładnie
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Recipes>()
        //        .HasOne(p => p.Step)
        //        .WithMany(b => b.Recipe)
        //        .HasForeignKey(p => p.StepId)
        //        .HasPrincipalKey(b => b.RecipeId);
        //}


    }
}
