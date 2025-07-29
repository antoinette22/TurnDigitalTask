using Microsoft.AspNetCore.Mvc;
using NewsApp.Domain.Interfaces;
using NewsApp.Domain.Models;
using NewsApp.ViewModels;
namespace NewsApp.Controllers
{
  
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Category/Manage
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var categories = await _categoryService.GetAllAsync();
            return View("Index", categories);
        }


        public IActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                // Map ViewModel to Domain Model
                var category = new Category
                {
                    Title = model.Title,
                    Description = model.Description
                };

                await _categoryService.AddAsync(category);
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var model = new CategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var category = new Category
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description
                };

                await _categoryService.Update(category);
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryService.Delete(category);
            return RedirectToAction("Manage");
        }
    }
}
