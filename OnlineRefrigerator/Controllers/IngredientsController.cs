using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        //działa ale do poprawy
        //[HttpGet]
        //public IActionResult Test()
        //{
        //    var name = HttpContext.Request.Query["term"].ToString();
        //    var ingredientName = _context.Ingredients.Where(c => c.Name.Contains(name)).Select(c => c.Name).ToList();
        //    return Ok(ingredientName);
        //}

        /// <summary>
        /// returns viewmodel with ingredients and categories
        /// </summary>
        /// <returns></returns>
        /// 
        //TODO: poprawić ten szajs
        public IngredientsCategoryViewModel GetIngredients(IngredientsFilter filters)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category).Include(i => i.Image)
                              select m;

            var categories = from m in _context.Categories
                           select m;

            var ingredientCategoryVM = new IngredientsCategoryViewModel
            {

                //Categories = _context.Categories
                //                    .Select(a => new SelectListItem()
                //                    {
                //                        Text = a.Name,
                //                        Value = a.Id.ToString()
                //                    }).ToList(),

                Categories = categories.ToList(),

                Ingredients = ingredients.ToList()

            };

            if (!string.IsNullOrEmpty(filters.IngredientName))
                ingredientCategoryVM.Ingredients = ingredientCategoryVM.Ingredients.Where(s => s.Name.ToLower().Contains(filters.IngredientName.ToLower())).ToList();
            //if (filters.CategoryId != 0)
            //    ingredientCategoryVM.Categories = ingredientCategoryVM.Categories.Where(s => s.Id == filters.CategoryId).ToList();




            return ingredientCategoryVM;

        }








        // GET: Ingredients
        //public Task<IActionResult> Index(int SelectedCategory, string searchString)
        public IActionResult Index(int SelectedCategory, string searchString)
        {

            //var VM = GetIngredients(new IngredientsFilter());

           
            return View();
            

        }

        [HttpPost]
        public IActionResult ShowIngredients(IngredientsFilter filters)
        {
            //IngredientsFilter filters

        

            var VM = GetIngredients(filters);

            return PartialView("~/Views/_IngredientsViewModel.cshtml",VM);

         
        }




        public ActionResult GetImage(int id)
        {

            var image = _context.IngredientsImages.Where(x => x.Id == id).FirstOrDefault();



            if(image!=null)
            {
                byte[] test = image.Image;

                return File(test, "image/jpg");
            }

            else
            {
                return null;
            }
            
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

            IngredientsImages ingredientImage = new IngredientsImages();

            var image = model.Image;

            ingredientModel.CategoryId = model.SelectedCategory;



            if (!ModelState.IsValid)
            {
                return View(model);
            }
                       

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        var imageToBeUploadedByteArray = memoryStream.ToArray();
                        ingredientImage.Image = imageToBeUploadedByteArray;
                                               
                    }
                }


                _context.IngredientsImages.Add(ingredientImage);
                await _context.SaveChangesAsync();

                ingredientModel.ImageId = ingredientImage.Id;
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
