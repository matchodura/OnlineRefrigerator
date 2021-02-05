using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class CalculatorViewModel
    {           
        public Ingredients Ingredient { get; set; }
        public SelectList ServingTypes { get; set; }
        public int SelectedServing{ get; set; }

        [DisplayName("Serving Quantity"), Range(1, Int32.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public int ServingQuantity { get; set; }

    }
}

