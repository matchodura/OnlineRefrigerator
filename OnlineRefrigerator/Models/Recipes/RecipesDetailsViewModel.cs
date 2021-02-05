using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesDetailsViewModel
    {

        public Recipes Recipe { get; set; }
        public List<RecipesSteps> RecipesSteps { get; set; }
        public List<IngredientsRecipes> IngredientsRecipes { get; set; }
        public List<IngredientsData> IngredientsUsed { get; set; }             
        public int UserVote { get; set; }
        public double Score { get; set; }
        public bool UserVoted { get; set; }
        public string VotingButton { get; set; }              
        [BindProperty]
        public int VoteValue { get; set; }             
        public int[] Values = new[] { 1, 2, 3, 4, 5 };             
    }

    public class IngredientsData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Quantity { get; set; }
    }
}
