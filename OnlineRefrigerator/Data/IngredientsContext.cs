﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<Ingredients> Ingredient { get; set; }

    }
}