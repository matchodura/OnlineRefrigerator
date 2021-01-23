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

        public List<IngredientsData> IngredientsUsed { get; set; }

        
    }

    public class IngredientsData
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public int? Quantity { get; set; }
    }

}
