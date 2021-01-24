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


        [HttpPost]
        public JsonResult Autocomplete(string prefix)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;


            return Json(ingredients);
        }

        [HttpPost]
        public IActionResult DisplayRecipes(int id)
        {

            var partialViewModel = GetRecipes(id);
            return PartialView("~/Views/Finder/_FinderDisplayRecipesPartial.cshtml", partialViewModel);


        }

        



        public FinderViewModel GetRecipes(int id)
        {

            FinderViewModel vm = new FinderViewModel()
            {
                Recipes = (from r in _context.IngredientsRecipes
                           join k in _context.Ingredients on r.IngredientId equals k.Id
                           join l in _context.Recipes on r.RecipeId equals l.Id                          
                           where r.IngredientId == id
                           select l).Include(a=>a.Type).ToList()

            };

            //FinderViewModel vm = new FinderViewModel()
            //{
            //    Recipes = _context.Recipes.ToList()

            //};


            return vm;

        }

        // GET: RecipeFinder
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecipeFinder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecipeFinder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipeFinder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeFinder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecipeFinder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RecipeFinder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecipeFinder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
