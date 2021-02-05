using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Controllers
{
    public class FinderController : Controller
    {

        private readonly IngredientsContext _context;


        public FinderController(IngredientsContext context)
        {
            _context = context;           
        }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Autocomplete(string prefix)
        {
            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;

            return Json(ingredients);
        }


        [HttpPost]
        public IActionResult DisplayRecipes(int[] ids, bool displayMissing, bool ignoreHerbs)
        {
            var partialViewModel = GetRecipes(ids, displayMissing, ignoreHerbs);
            return PartialView("~/Views/Finder/_FinderDisplayRecipesPartial.cshtml", partialViewModel);
        }


        public FinderViewModel GetRecipes(int[] ids, bool displayMissing, bool ignoreHerbs)
        {
            var query = _context.IngredientsRecipes.ToList();
        
            var results = query
              .GroupBy(k => k.RecipeId)
              .Where(g => ids.All(w => g.Any(k => w.Equals(k.IngredientId))))
              .Select(g => g.Key);

            var value = new List<Recipes>();


            if (displayMissing == true)
            {
                value = (from r in _context.IngredientsRecipes
                         join k in _context.Ingredients on r.IngredientId equals k.Id
                         join l in _context.Recipes on r.RecipeId equals l.Id
                         where ids.Contains(r.IngredientId)
                         select l).Include(a => a.Type).ToList();
            }


            else
            {
                 value = _context.Recipes.Where(r => results.Contains(r.Id)).Include(a => a.Type).ToList();
            }


            FinderViewModel vm = new FinderViewModel()
            {
                Recipes = value
            };


            return vm;
        }
              
        
        public ActionResult Details(int? id)
        {       
            return RedirectToAction("Details", "Recipes", new { id = id});
        }
           
    }
}
