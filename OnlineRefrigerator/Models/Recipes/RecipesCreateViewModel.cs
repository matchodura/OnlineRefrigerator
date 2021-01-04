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


    public class RecipesCreateViewModel
    {
        public Recipes Recipe { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int SelectedCategory { get; set; }
              


        public List<Step> StepList{ get; set; }


        public RecipesCreateViewModel()
        {

            StepList = new List<Step>();

        }



        //TODO: image upload dla przepisu
        //public IFormFile Image { get; set; }
    }


}

