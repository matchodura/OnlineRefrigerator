using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesDetailsViewModel
    {

        public Recipes Recipe { get; set; }

        public List<RecipesSteps> RecipesSteps { get; set; }

        public List<IngredientsRecipes> IngredientsRecipes { get; set; }

    }
}
