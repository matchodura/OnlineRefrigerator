using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace OnlineRefrigerator.Models
{
    public class IngredientsCreateViewModel
    {

        public Ingredients Ingredient { get; set; }

        [DisplayName("Category")]
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Servings { get; set; }

        public int SelectedCategory { get; set; }
        public int SelectedServing { get; set; }


        [DisplayName("Grams per serving"), Range(0.0, Double.MaxValue, ErrorMessage = "Please provide serving value in grams e.g one banana has 120 grams")]
        public int ServingValue { get; set; }

        public IFormFile Image { get; set; }
    }
}
