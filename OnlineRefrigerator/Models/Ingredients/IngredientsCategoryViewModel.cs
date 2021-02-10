﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class IngredientsCategoryViewModel
    {
        public List<Ingredients> Ingredients { get; set; }
        public List<IngredientsCategories> Categories { get; set; }     
    }
}
