using System;
using System.Collections.Generic;
using System.Linq;
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
            //_ingredientsContext = ingredientsContext;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(int SelectedCategory)
        {

            var recipes = from m in _context.Recipes.Include(x => x.Type)
                          select m;



            if (SelectedCategory != 0)
            {
                recipes = recipes.Where(x => x.Type.Id == SelectedCategory);
            }

            var recipesCategoryVM = new RecipesCategoryViewModel
            {

                Categories = _context.RecipesCategories
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.Name,
                                        Value = a.Id.ToString()
                                    }).ToList(),

                Recipes = await recipes.ToListAsync()


            };



            return View(recipesCategoryVM);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var vm = new RecipesDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.Include(x=>x.Type).FirstOrDefaultAsync(m => m.Id == id);

            var steps = _context.RecipesSteps.Where(s => s.RecipeId == id).ToList();




            var ingredients = (from s in _context.Ingredients
                        join c in _context.IngredientsRecipes on s.Id equals c.IngredientId
                        where c.RecipeId == id
                        select s).ToList();



            
           

            if (recipe == null)
            {
                return NotFound();
            }

            vm.IngredientsUsed = ingredients;
            vm.Recipe = recipe;
            vm.RecipesSteps = steps;


            return View(vm);
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


        //returns ingredient names from database as json objects for autocomplete searching
        [HttpPost]
        public JsonResult Autocomplete(string prefix)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;





            return Json(ingredients);
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
                 

                    _context.Add(ingredientsRecipes);

                }
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
