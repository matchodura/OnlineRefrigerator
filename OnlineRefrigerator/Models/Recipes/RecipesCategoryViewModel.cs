using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesCategoryViewModel
    {
        public List<Recipes> Recipes { get; set; }

        public List<RecipesCategories> Categories { get; set; }

        public int SelectedCategory { get; set; }


    }
}
