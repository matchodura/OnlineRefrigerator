using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class IngredientsCreateViewModel
    {

        public Ingredients Ingredient { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public int SelectedCategory { get; set; }
    }
}
