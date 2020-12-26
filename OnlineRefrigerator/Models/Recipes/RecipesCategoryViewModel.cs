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

        public List<SelectListItem> Categories { get; set; }

        public int SelectedCategory { get; set; }

            
        //TODO: wyszukiwanie poprzez nazwę potrawy
        //public string SearchString { get; set; }


    }
}
