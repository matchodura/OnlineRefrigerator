using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IngredientsContext _context;
        private readonly RecipesContext _recipesContext;

        public CalculatorController(IngredientsContext context, RecipesContext recipesContext)
        {
            _recipesContext = recipesContext;
            _context = context;
        }
        // GET: Calculator
        public IActionResult Index()
        {        


            return View();

        }


        public CalculatorViewModel GetIngredients(CalculatorFilter filters)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              select m;

            

            var recipes = from m in _recipesContext.Recipes.Include(x => x.Type)
                          select m;

           


            var calculatorVM = new CalculatorViewModel()
            {

                Recipes = recipes.ToList(),
                Ingredients = ingredients.ToList(),
                
                                               
            };


        




            if (filters.TypeId == 0)
            {                                              
                calculatorVM.Ingredients = calculatorVM.Ingredients.Where(s => s.CategoryId == filters.CategoryId).ToList();

                var items = _context.Ingredients.Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.Category.Name
                }).Distinct().ToList();

                calculatorVM.Glossary_List = items;

                foreach (var item in calculatorVM.Ingredients)
                {
                    var results = new Results();

                    results.Name = item.Name;
                    results.Id = item.Id;
                    results.EnergyValues = item.Energy;
                    results.CategoryId = item.CategoryId;
                    results.CategoryName = item.Category.Name;

                    calculatorVM.Results.Add(results);



                }

            }
            else
            {
                calculatorVM.Recipes = calculatorVM.Recipes.Where(s => s.TypeId == filters.CategoryId).ToList();

                var items = _recipesContext.Recipes.Select(x => new SelectListItem
                {
                    Value = x.TypeId.ToString(),
                    Text = x.Type.Name
                }).Distinct().ToList();

                calculatorVM.Glossary_List = items;

                foreach (var item in calculatorVM.Recipes)
                {
                    var results = new Results();

                    results.Name = item.Name;
                    results.Id = item.Id;
                    results.CategoryId = (int)item.TypeId;
                    results.CategoryName = item.Type.Name;

                    calculatorVM.Results.Add(results);

                }

            }


            
         
           

            return (calculatorVM);
        }


        [HttpPost]
        public IActionResult ShowCategories(CalculatorFilter filters)
        {

            var partialViewModel = GetIngredients(filters);

            return PartialView("~/Views/Calculator/_CalculatorCategoriesPartial.cshtml",partialViewModel);
        }



        [HttpPost]
        public IActionResult ShowIngredients(CalculatorFilter filters)
        {

            var partialViewModel = GetIngredients(filters);

            return PartialView("~/Views/Calculator/_CalculatorResultsPartial.cshtml", partialViewModel);

        }

        // GET: Calculator/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calculator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculator/Create
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

        // GET: Calculator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calculator/Edit/5
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

        // GET: Calculator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calculator/Delete/5
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
