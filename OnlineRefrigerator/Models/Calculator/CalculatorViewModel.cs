using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class CalculatorViewModel
    {

        public List<Ingredients> Ingredients { get; set; }

        public Ingredients Ingredient { get; set; }




        public List<Recipes> Recipes { get; set; }


        public List<SelectListItem> IngredientsCategories { get; set; }
        public List<SelectListItem> RecipesCategories { get; set; }
        public List<SelectListItem> Servings { get; set; }


        public int SelectedType { get; set; }
        public int SelectedServing { get; set; }

        public int SelectedCategory { get; set; }

        public List<Results> Results { get; set; }


        public string CategoryName { get; set; }



        public string Selected_Glossary { get; set; }
        public List<SelectListItem> Glossary_List { get; set; }

        public CalculatorViewModel()
        {

            Results = new List<Results>();
        }

    }

    public class Results
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public decimal EnergyValues { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

    }

   
}
