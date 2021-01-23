using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{

    public class Step
    {
      
        public string Text{ get; set; }

    }

    public class Ingredient
    {
        public int Id{ get; set; }

        public string Name { get; set; }
            
        public int ServingId { get; set; }

        public int ServingQuantity { get; set; }

        public string ServingType { get; set; }
               
    }

    public class RecipesCreateViewModel
    {
        public Recipes Recipe { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int SelectedCategory { get; set; }              
      
        public List<Step> StepList{ get; set; }

        public List<Ingredient> IngredientsList { get; set; }



        public RecipesCreateViewModel()
        {

            StepList = new List<Step>();
            IngredientsList = new List<Ingredient>();

        }

    }


}

