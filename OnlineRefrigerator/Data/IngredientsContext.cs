using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Data
{
    public class IngredientsContext : DbContext
    {

        public IngredientsContext(DbContextOptions<IngredientsContext> options) : base(options) { }

        public DbSet<Ingredients> Ingredients { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Servings> Servings { get; set; }

        public DbSet<IngredientsImages> IngredientsImages { get; set; }
       

    }
}
