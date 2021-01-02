using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesCreateViewModel
    {
        public Recipes Recipe { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int SelectedCategory { get; set; }


        public List<RecipesSteps> RecipesSteps { get; set; }


        public RecipesCreateViewModel()
        {

            RecipesSteps = new List<RecipesSteps>();

        }



        //TODO: image upload dla przepisu
        //public IFormFile Image { get; set; }
    }


}

