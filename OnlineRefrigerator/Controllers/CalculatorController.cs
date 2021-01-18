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


        [HttpPost]
        public JsonResult Index(string prefix)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;                     

            return Json(ingredients);
        }


        public Ingredients GetDetails(int? id)
        {
            
            var ingredient =  _context.Ingredients.Include(x => x.Category).Include(x => x.Serving)
                .FirstOrDefault(m => m.Id == id);

            return ingredient;
        }


        [HttpPost]
        public IActionResult DisplayDetails(int id)
        {

            var partialViewModel = GetDetails(id);

            return PartialView("~/Views/Calculator/_CalculatorIngredientsDetailsPartial", partialViewModel);
        }





        //public CalculatorViewModel GetIngredients(CalculatorFilter filters)
        //{

        //    var ingredients = from m in _context.Ingredients.Include(x => x.Category)
        //                      select m;



        //    var recipes = from m in _recipesContext.Recipes.Include(x => x.Type)
        //                  select m;




        //    var calculatorVM = new CalculatorViewModel()
        //    {

        //        Recipes = recipes.ToList(),
        //        Ingredients = ingredients.ToList(),


        //    };


        //    if (filters.TypeId == 0)
        //    {
        //        //calculatorVM.Ingredients = calculatorVM.Ingredients.Where(s => s.CategoryId == filters.CategoryId).ToList();

        //        //var items = _context.Ingredients.Select(x => new SelectListItem
        //        //{
        //        //    Value = x.CategoryId.ToString(),
        //        //    Text = x.Category.Name
        //        //}).Distinct().ToList();

        //        //calculatorVM.Glossary_List = items;

        //        calculatorVM.Ingredients = calculatorVM.Ingredients.Where(s => s.CategoryId == filters.CategoryId).ToList();

        //        var items = _context.Ingredients.Select(x => new SelectListItem
        //        {
        //            Value = x.CategoryId.ToString(),
        //            Text = x.Category.Name
        //        }).Distinct().ToList();

        //        calculatorVM.Glossary_List = items;


        //        foreach (var item in calculatorVM.Ingredients)
        //        {
        //            var results = new Results();

        //            results.Name = item.Name;
        //            results.Id = item.Id;
        //            results.EnergyValues = item.Energy;
        //            results.CategoryId = item.CategoryId;
        //            results.CategoryName = item.Category.Name;

        //            calculatorVM.Results.Add(results);



        //        }

        //    }
        //    else
        //    {
        //        calculatorVM.Recipes = calculatorVM.Recipes.Where(s => s.TypeId == filters.CategoryId).ToList();

        //        var items = _recipesContext.Recipes.Select(x => new SelectListItem
        //        {
        //            Value = x.TypeId.ToString(),
        //            Text = x.Type.Name
        //        }).Distinct().ToList();

        //        calculatorVM.Glossary_List = items;

        //        foreach (var item in calculatorVM.Recipes)
        //        {
        //            var results = new Results();

        //            results.Name = item.Name;
        //            results.Id = item.Id;
        //            results.CategoryId = (int)item.TypeId;
        //            results.CategoryName = item.Type.Name;

        //            calculatorVM.Results.Add(results);

        //        }

        //    }






        //    return (calculatorVM);
        //}








        //[HttpPost]
        //public IActionResult ShowCategories(CalculatorFilter filters)
        //{

        //    var partialViewModel = GetIngredients(filters);

        //    return PartialView("~/Views/Calculator/_CalculatorCategoriesPartial.cshtml",partialViewModel);
        //}



        //[HttpPost]
        //public IActionResult ShowIngredients(CalculatorFilter filters)
        //{

        //    var partialViewModel = GetIngredients(filters);

        //    return PartialView("~/Views/Calculator/_CalculatorResultsPartial.cshtml", partialViewModel);

        //}

        //// GET: Calculator/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}




    }
}
