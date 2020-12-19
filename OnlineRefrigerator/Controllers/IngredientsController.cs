using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;

namespace OnlineRefrigerator
{
    public class IngredientsController : Controller
    {
        private readonly IngredientsContext _context;

        public IngredientsController(IngredientsContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index(string ingredientCategory, string searchString)
        {

            // Use LINQ to get list of genres.
            //IQueryable<string> categoryQuery = from m in _context.Ingredients
            //                                   orderby m.Category
            //                                   select m.Category;

            // var ingredients = from m in _context.Ingredients
            //                   select m;

            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    ingredients = ingredients.Where(s => s.Name.Contains(searchString));
            //}

            //if (!string.IsNullOrEmpty(ingredientCategory))
            //{
            //    ingredients = ingredients.Where(x => x.Category == ingredientCategory);
            //}




            //var ingredientCategoryVM = new IngredientsCategoryViewModel
            //{
            //    Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
            //    Ingredients = await ingredients.ToListAsync()
            //};

            //return View(ingredientCategoryVM);

            

            var ingredients = from m in _context.Ingredients.Include(x=>x.Category)
                              select m;

            IQueryable<string> categoryQuery = from m in _context.Categories
                                               orderby m.Name
                                               select m.Name;
            ViewBag.CategoryName = new SelectList(categoryQuery);

            var ingredientCategoryVM = new IngredientsCategoryViewModel
            {
               
                Categories = new SelectList(await categoryQuery.ToListAsync()),
             
                Ingredients = await ingredients.ToListAsync()
            };

            

            //Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");

            return View(ingredientCategoryVM);

        }

        // GET: Ingredients/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: Ingredients/Create
       
        public IActionResult Create()
        {
            var vm = new IngredientsCreateViewModel();

            vm.Categories = _context.Categories
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.Name,
                                        Value = a.Id.ToString()
                                    }).ToList();

            return View(vm);
        }




        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //[Bind("Id,Category,Name,Fat,Carbs,Protein,Energy,CategoryId")] Ingredients ingredients
        public async Task<IActionResult> Create(IngredientsCreateViewModel model)
        {

            Ingredients ingredientModel = model.Ingredient;

            ingredientModel.CategoryId = model.SelectedCategory;


            if (ModelState.IsValid)
            {
                _context.Add(ingredientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredientModel);
        }

        // GET: Ingredients/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Name,Fat,Carbs,Protein,Energy")] Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.Id))
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
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
