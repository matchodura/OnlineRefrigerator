﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class IngredientsCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ingredients> Ingredient { get; set; }
    }
}
