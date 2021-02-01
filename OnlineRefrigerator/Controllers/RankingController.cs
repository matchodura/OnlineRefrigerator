using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Controllers
{
    public class RankingController : Controller
    {

        
        private readonly IngredientsContext _context;
     

        public RankingController(IngredientsContext context)
        {
            _context = context;
        }


        // GET: RatingController
        public ActionResult Index()
        {
          
            return View();
        }


        public List<RankingViewModel> GetResults(RecipesFilter filters)
        {


            var results = (from recipe in _context.Recipes
                          select new RankingViewModel { Id = recipe.Id, Name = recipe.Name, Score = Math.Round((double)((float)recipe.VoteValue / recipe.VoteCounts), 2) }).ToList();


            if (filters.ColumnName != null)
            {
                //geting property name of ingredient class, selected by clicking on the sort button of specified column name in partial view
                PropertyInfo orderByProperty = typeof(RankingViewModel).GetProperties().SingleOrDefault(property => property.Name == filters.ColumnName);

                if (filters.SortOrder)
                {
                  
                     results = results.OrderByDescending(s => orderByProperty.GetValue(s)).ToList();

                }

                else if (!filters.SortOrder)
                {

                    results = results.OrderBy(s => orderByProperty.GetValue(s)).ToList();

                }

            }

            return results;

        }


        [HttpPost]
        public IActionResult ShowResults(RecipesFilter filters)
        {

            var partialViewModel = GetResults(filters);

            return PartialView("~/Views/Ranking/_RankingResultsPartial.cshtml", partialViewModel);

        }


        // GET: RecipeFinder/Details/5
        public ActionResult Details(int? id)
        {
            //return View();
            return RedirectToAction("Details", "Recipes", new { id = id });
        }
              
    }
}
