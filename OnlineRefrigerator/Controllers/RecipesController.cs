using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;

namespace OnlineRefrigerator.Controllers
{
    public class RecipesController : Controller
    {
        //private readonly RecipesContext _context;
        private readonly IngredientsContext _context;

        public RecipesController(IngredientsContext context)
        {
            _context = context;           
        }

        // GET: Recipes       
        public IActionResult Index()
        {

            return View();

        }


        //returns ingredient names from database as json objects for autocomplete searching
        [HttpPost]
        public JsonResult AutocompleteFindRecipe(string prefix)
        {

            var recipes = from m in _context.Recipes.Include(x => x.Type)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;

            return Json(recipes);
        }



        [HttpPost]
        public JsonResult AutocompleteFindIngredient(string prefix)
        {

            var ingredients = from m in _context.Ingredients
                          where m.Name.StartsWith(prefix)
                          select new { m.Name, m.Id }; ;

            return Json(ingredients);
        }

        /// <summary>
        /// returns partial view with viewmodel of recipes and categories
        /// </summary>
        /// <returns></returns>
        ///    
        public RecipesCategoryViewModel GetRecipes(RecipesFilter filters)
        {


            var recipes = from m in _context.Recipes.Include(x => x.Type)
                              select m;

            var categories = from m in _context.RecipesCategories
                             select m;


          

            var recipeVM = new RecipesCategoryViewModel
            {

             
                Categories = categories.ToList(),


                Recipes = recipes.ToList()

              

            };


            if (!string.IsNullOrEmpty(filters.RecipeName))
                recipeVM.Recipes = recipeVM.Recipes.Where(s => s.Name.ToLower().StartsWith(filters.RecipeName.ToLower())).ToList();

            if (filters.CategoryId != 0)
                recipeVM.Recipes = recipeVM.Recipes.Where(s => s.TypeId == filters.CategoryId).ToList();


            if (filters.ColumnName != null)
            {
                //geting property name of ingredient class, selected by clicking on the sort button of specified column name in partial view
                PropertyInfo orderByProperty = typeof(Recipes).GetProperties().SingleOrDefault(property => property.Name == filters.ColumnName);

                if (filters.SortOrder)
                {
                    var result = recipeVM.Recipes.OrderByDescending(s => orderByProperty.GetValue(s)).ToList();
                    recipeVM.Recipes = result;
                }

                else if (!filters.SortOrder)
                {
                    var result = recipeVM.Recipes.OrderBy(s => orderByProperty.GetValue(s)).ToList();
                    recipeVM.Recipes = result;
                }

            }

            return recipeVM;

        }



        [HttpPost]
        public IActionResult ShowRecipes(RecipesFilter filters)
        {

            var partialViewModel = GetRecipes(filters);

            return PartialView("~/Views/Recipes/_RecipesResultsPartial.cshtml", partialViewModel);

        }



        // GET: Recipes/Details
        public async Task<IActionResult> Details(int? id)
        {
            var vm = new RecipesDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.Include(x=>x.Type).FirstOrDefaultAsync(m => m.Id == id);

            var steps = _context.RecipesSteps.Where(s => s.RecipeId == id).ToList();

          


            List<IngredientsData> ingredients = (from s in _context.IngredientsRecipes
                                                join c in _context.Ingredients on s.IngredientId equals c.Id
                                                join k in _context.Servings on s.ServingType equals k.Id
                                                where s.RecipeId == id
                                                select new IngredientsData { Name = c.Name, Quantity = s.ServingQuantity, Type = k.ServingType }).ToList();
                                              

            vm.IngredientsUsed = ingredients;
            vm.Recipe = recipe;
            vm.RecipesSteps = steps;


            return View(vm);
        }



    

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVote(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes.FindAsync(id);


            if (recipes == null)
            {
                return NotFound();
            }





            recipes.VoteCounts = recipes.VoteCounts + 1;
            recipes.VoteValue = recipes.VoteValue + 5;


            if (id != recipes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipesExists(recipes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipes);
        }

        public IActionResult Create()
        {
            var vm = new RecipesCreateViewModel() { };

            vm.Categories = _context.RecipesCategories
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.Name,
                                        Value = a.Id.ToString()
                                    }).ToList();
                                

            return View(vm);
        }

               

        //get declared servings for selected ingredient
        [HttpPost]
        public JsonResult GetServingTypes(int id)
        {

            var ingredients = _context.Ingredients.Include(x => x.Category).Include(x => x.Serving)
                .FirstOrDefault(m => m.Id == id);

            var originalServing = ingredients.Serving;

            var serving = new List<ServingDropdownListItem>();

            serving.Add(new ServingDropdownListItem() { name = originalServing.ServingType, id = originalServing.Id });
            serving.Add(new ServingDropdownListItem() { name = "Grams", id = 5 });


            return Json(serving);
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipesCreateViewModel model)
        {

            Recipes recipe = model.Recipe;
            recipe.TypeId = model.SelectedCategory;            

            if (ModelState.IsValid)
            {

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                              
                             
                for(int i = 0; i<model.StepList.Count; i++)
                {

                    RecipesSteps recipesSteps = new RecipesSteps();

                    recipesSteps.RecipeId = recipe.Id;
                    recipesSteps.StepNumber = i;
                    recipesSteps.Text = model.StepList[i].Text;

                    _context.Add(recipesSteps);

                }


                for (int i = 0; i < model.IngredientsList.Count; i++)
                {

                    IngredientsRecipes ingredientsRecipes = new IngredientsRecipes();

                    ingredientsRecipes.RecipeId = recipe.Id;
                    ingredientsRecipes.IngredientId = model.IngredientsList[i].Id;
                    ingredientsRecipes.ServingType = model.IngredientsList[i].ServingId;
                    ingredientsRecipes.ServingQuantity = model.IngredientsList[i].ServingQuantity;

                    _context.Add(ingredientsRecipes);

                }
                //watiting for other queries to update date before inserting into main Recipes table
                await _context.SaveChangesAsync();

                
                return RedirectToAction(nameof(Index));
            }


            return View(recipe);
        }


        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes.FindAsync(id);
            if (recipes == null)
            {
                return NotFound();
            }
            return View(recipes);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,PreparationTime")] Recipes recipes)
        {
            if (id != recipes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipesExists(recipes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipes);
        }


        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipes = await _context.Recipes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipes == null)
            {
                return NotFound();
            }

            return View(recipes);
        }


        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipes = await _context.Recipes.FindAsync(id);
            _context.Recipes.Remove(recipes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipesExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
