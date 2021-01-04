using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class CalculatorViewModel
    {

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Servings { get; set; }

        public int SelectedCategory { get; set; }
        public int SelectedServing { get; set; }



    }
}
