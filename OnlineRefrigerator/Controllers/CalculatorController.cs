﻿using Microsoft.AspNetCore.Http;
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


        //returns ingredient names from database as json objects for autocomplete searching
        [HttpPost]
        public JsonResult Index(string prefix)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;                     

            return Json(ingredients);
        }




        //displaying partial view with ingredient details and options for calculating values
        [HttpPost]
        public IActionResult DisplayDetails(int id)
        {

            var partialViewModel = GetDetails(id);

            return PartialView("~/Views/Calculator/_CalculatorDetailsPartial.cshtml", partialViewModel);
        }


        //returning details of found ingredient
        public CalculatorViewModel GetDetails(int? id)
        {
            
            var ingredient =  _context.Ingredients.Include(x => x.Category).Include(x => x.Serving)
                .FirstOrDefault(m => m.Id == id);

         
            var model = new CalculatorViewModel
            {
                Ingredient = ingredient,
                ServingTypes = new SelectList(
                    new List<SelectListItem>
                    {
                        new SelectListItem { Text = ingredient.Serving.ServingType, Value = "portion" },
                        new SelectListItem { Text = "Grams", Value = "grams" }
                    }, "Value", "Text")
            };


            return model;
        }


        [HttpPost]
        public IActionResult DisplayResults(CalculatorParameters calculatorParameters)
        {

            var partialViewModel = CalculateResults(calculatorParameters);

            return PartialView("~/Views/Calculator/_CalculatorResultsPartial.cshtml", partialViewModel);
        }


        //helper method for calculating values with provided count and quantity
        public Ingredients CalculateResults(CalculatorParameters model)
        {

            Ingredients ingredientModel = new Ingredients();

            decimal calculatedFat = 0;
            decimal calculatedCarbs = 0;
            decimal calculatedProtein = 0;
            decimal calculatedEnergy = 0;



            if (model.ServingType == "portion")
            {
                calculatedFat = model.Fat * model.ServingQuantity;
                calculatedCarbs = model.Carbs * model.ServingQuantity;
                calculatedProtein = model.Protein * model.ServingQuantity;
                calculatedEnergy = model.Energy * model.ServingQuantity;

            }

            else
            {
                calculatedFat = model.Fat *  model.ServingQuantity / model.ServingValue;
                calculatedCarbs = model.Carbs * model.ServingQuantity / model.ServingValue;
                calculatedProtein = model.Protein * model.ServingQuantity / model.ServingValue;
                calculatedEnergy = model.Energy * model.ServingQuantity / model.ServingValue;

            }



            

         

            ingredientModel.Fat = calculatedFat;
            ingredientModel.Carbs = calculatedCarbs;
            ingredientModel.Protein = calculatedProtein;
            ingredientModel.Energy = calculatedEnergy;

        




            return ingredientModel;
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
