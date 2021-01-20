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
        [Required(ErrorMessage = "Name must be provided")]
        public string Text{ get; set; }

    }

    public class Ingredient
    {
        public int Id{ get; set; }

        public string Name { get; set; }

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



        //TODO: image upload dla przepisu
        //public IFormFile Image { get; set; }
    }


}

