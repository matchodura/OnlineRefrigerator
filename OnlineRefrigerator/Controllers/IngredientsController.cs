using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRefrigerator.Data;
using OnlineRefrigerator.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineRefrigerator
{
    public class IngredientsController : Controller
    {
        private readonly IngredientsContext _context;
        private readonly IWebHostEnvironment env;
        
        public IngredientsController(IngredientsContext context, IWebHostEnvironment env)
        {
            //allows for using wwwroot files
            this.env = env;
            _context = context;
        }


        /// <summary>
        /// returns partial view with viewmodel of ingredients and categories
        /// </summary>
        /// <returns></returns>
        ///    
        public IngredientsCategoryViewModel GetIngredients(IngredientsFilter filters)
        {


            var ingredients = from m in _context.Ingredients.Include(x => x.Category).Include(i => i.Image).Include(x=>x.Serving)
                              select m;

            var categories = from m in _context.Categories
                           select m;


            //var servings = from m in _context.Servings.Include(x => x.ServingType)
            //               select m;



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

                //Servings = servings.ToList()

            };


            if (!string.IsNullOrEmpty(filters.IngredientName))
                ingredientCategoryVM.Ingredients = ingredientCategoryVM.Ingredients.Where(s => s.Name.ToLower().StartsWith(filters.IngredientName.ToLower())).ToList();
           
            if (filters.CategoryId != 0)
                ingredientCategoryVM.Ingredients = ingredientCategoryVM.Ingredients.Where(s => s.CategoryId == filters.CategoryId).ToList();

           
            if (filters.ColumnName != null)
            {
                //geting property name of ingredient class, selected by clicking on the sort button of specified column name in partial view
                PropertyInfo orderByProperty = typeof(Ingredients).GetProperties().SingleOrDefault(property => property.Name == filters.ColumnName);

                if (filters.SortOrder)
                {
                    var result = ingredientCategoryVM.Ingredients.OrderByDescending(s => orderByProperty.GetValue(s)).ToList();
                    ingredientCategoryVM.Ingredients = result;
                }

                else if (!filters.SortOrder)
                {
                    var result = ingredientCategoryVM.Ingredients.OrderBy(s => orderByProperty.GetValue(s)).ToList();
                    ingredientCategoryVM.Ingredients = result;
                }

            }

            return ingredientCategoryVM;

        }



        [HttpPost]
        public JsonResult Autocomplete(string prefix)
        {

            var ingredients = from m in _context.Ingredients.Include(x => x.Category)
                              where m.Name.StartsWith(prefix)
                              select new { m.Name, m.Id }; ;


            return Json(ingredients);
        }




        // GET: Ingredients

        public IActionResult Index()
        {
                       
            return View();
            
        }

        [HttpPost]
        public IActionResult ShowIngredients(IngredientsFilter filters)
        {
                  
            var partialViewModel = GetIngredients(filters);

            return PartialView("~/Views/_IngredientsResultsPartial.cshtml", partialViewModel);
                     
        }




        public ActionResult GetImage(int id)
        {
            //selectes images from db
            var ingredientImage = _context.IngredientsImages.Where(x => x.Id == id).FirstOrDefault();

            //gets path of immage in wwwroot
            var path = env.WebRootFileProvider.GetFileInfo("Images/missing_image.jpg")?.PhysicalPath;


           


            if(ingredientImage != null)
            {
                
                if(ingredientImage.Image == null)
                {
                    var imageFileStream = System.IO.File.OpenRead(path);
                    return File(imageFileStream, "image/jpeg");

                }

                else
                {
                    byte[] image = ingredientImage.Image;

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



        // GET: Ingredients/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients.Include(x => x.Category).Include(x=>x.Serving)
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
            var vm = new IngredientsCreateViewModel()
            {
                Categories = _context.Categories
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.Name,
                                        Value = a.Id.ToString()
                                    }).ToList(),


                Servings = _context.Servings
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.ServingType,
                                        Value = a.Id.ToString()
                                    }).ToList()


            };

           

            return View(vm);
        }


        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public async Task<IActionResult> Create(IngredientsCreateViewModel model)
        {

            Ingredients ingredientModel = model.Ingredient;

            IngredientsImages ingredientImage = new IngredientsImages();

            var image = model.Image;

            ingredientModel.CategoryId = model.SelectedCategory;

            ingredientModel.ServingId = model.SelectedServing;
            ingredientModel.ServingValue = model.ServingValue;



            if (!ModelState.IsValid)
            {

                model.Categories = _context.Categories
                                    .Select(a => new SelectListItem()
                                    {
                                        Text = a.Name,
                                        Value = a.Id.ToString()
                                    }).ToList();


                model.Servings = _context.Servings
                                     .Select(a => new SelectListItem()
                                     {
                                         Text = a.ServingType,
                                         Value = a.Id.ToString()
                                     }).ToList();


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
