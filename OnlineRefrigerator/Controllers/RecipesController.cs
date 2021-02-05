using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Areas.Identity.Data;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;

namespace OnlineRefrigerator.Controllers
{
    public class RecipesController : Controller
    {    
        
        private readonly IngredientsContext _context;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<AppUser> _userManager;


        public RecipesController(IngredientsContext context, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            //allows for using wwwroot files
            this.env = env;
            _context = context;
            _userManager = userManager;
        }

          
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
        public RecipesCategoryViewModel GetRecipes(SortingFilter filters)
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

            if (!string.IsNullOrEmpty(filters.Name))
                recipeVM.Recipes = recipeVM.Recipes.Where(s => s.Name.ToLower().StartsWith(filters.Name.ToLower())).ToList();

            if (filters.Category != 0)
                recipeVM.Recipes = recipeVM.Recipes.Where(s => s.TypeId == filters.Category).ToList();

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
        public IActionResult ShowRecipes(SortingFilter filters)
        {
            var partialViewModel = GetRecipes(filters);
            return PartialView("~/Views/Recipes/_RecipesResultsPartial.cshtml", partialViewModel);
        }

           
        public async Task<IActionResult> Details(int? id)
        {
            var vm = new RecipesDetailsViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

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

            vm.UserVote = await _context.UserVotes.Where(x => x.UserId == userId && x.RecipeId == id).Select(a => a.VoteValue).FirstOrDefaultAsync();

            if (_context.UserVotes.Where(u => u.UserId == userId && u.RecipeId == recipe.Id).Any())
            {
                vm.VotingButton = "Recast Vote!";
            }
            else
            {
                vm.VotingButton = "Cast Vote!";
            }


            if (recipe.VoteValue != null)
            {
                vm.Score = Math.Round((double)((float)recipe.VoteValue / recipe.VoteCounts), 2);
            }

            else
            {
                vm.Score = 0;
            }

            vm.IngredientsUsed = ingredients;
            vm.Recipe = recipe;
            vm.RecipesSteps = steps;                      

            return View(vm);
        }

               
        [HttpPost]
        [ValidateAntiForgeryToken]           
        public async Task<IActionResult> CastVote(RecipesDetailsViewModel model)
        {

            //get current logged user id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            //get current recipe
            var currentRecipe = await _context.Recipes.FindAsync(model.Recipe.Id);

            //get current user votes
            var userVotes = _context.UserVotes.Where(x => x.RecipeId == model.Recipe.Id && x.UserId == userId).SingleOrDefault();


            //if user changes his vote
            if ( _context.UserVotes.Where(u=> u.UserId == userId && u.RecipeId == currentRecipe.Id).Any())
            {                           
                var recipe = await _context.Recipes.FindAsync(model.Recipe.Id);

                if (userVotes.VoteValue - model.VoteValue >=0)
                {
                    recipe.VoteValue = currentRecipe.VoteValue - (userVotes.VoteValue - model.VoteValue);
                }

                else
                {
                    recipe.VoteValue = currentRecipe.VoteValue + (model.VoteValue - userVotes.VoteValue );
                }
                
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

                userVotes.VoteValue = model.VoteValue;

                _context.UserVotes.Update(userVotes);
                await _context.SaveChangesAsync();

            }

            // if first vote by user or first vote for recipe
            else
            {                     
                var recipe = currentRecipe;

                if (currentRecipe.VoteCounts == null)
                {
                    var voteCounts = 0;
                    var voteValue = 0;

                    voteCounts += 1;
                    voteValue = voteValue + model.VoteValue;

                    recipe.VoteCounts = voteCounts;
                    recipe.VoteValue = voteValue;      
                }

                else
                {
                    var voteCounts = currentRecipe.VoteCounts;
                    var voteValue = currentRecipe.VoteValue;

                    voteValue = voteValue + model.VoteValue;
                    voteCounts += 1;

                    recipe.VoteCounts = voteCounts;
                    recipe.VoteValue = voteValue;
                }

                var votes = new UserVotes();
                votes.RecipeId = model.Recipe.Id;
                votes.VoteValue = model.VoteValue;
                votes.UserId = userId;

                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();

                _context.UserVotes.Add(votes);
                await _context.SaveChangesAsync();

            }                        

            return RedirectToAction("Details", new { id = model.Recipe.Id });
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


        public ActionResult GetImage(int id)
        {
            //selectes images from db
            var recipeImage = _context.RecipesImages.Where(x => x.Id == id).FirstOrDefault();

            //gets path of immage in wwwroot
            var path = env.WebRootFileProvider.GetFileInfo("Images/missing_image.jpg")?.PhysicalPath;


            if (recipeImage != null)
            {

                if (recipeImage.Image == null)
                {
                    var imageFileStream = System.IO.File.OpenRead(path);
                    return File(imageFileStream, "image/jpeg");

                }

                else
                {
                    byte[] image = recipeImage.Image;

                    return File(image, "image/jpg");


                }


            }

            else
            {

                //displays missing image as placeholder if correct image was not provided
                var imageFileStream = System.IO.File.OpenRead(path);
                return File(imageFileStream, "image/jpeg");

            }

        }

          
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipesCreateViewModel model)
        {

            Recipes recipe = model.Recipe;
            RecipesImages recipeImage = new RecipesImages();

            var image = model.Image;
            recipe.TypeId = model.SelectedCategory;

            if (!ModelState.IsValid)
            {               
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if( image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        var imageToBeUploadedByteArray = memoryStream.ToArray();
                        recipeImage.Image = imageToBeUploadedByteArray;
                    }
                }

                _context.RecipesImages.Add(recipeImage);
                await _context.SaveChangesAsync();

                recipe.ImageId = recipeImage.Id;
                
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
