using Microsoft.AspNetCore.Mvc;
using NewsApp.ApplicationLayer.Interfaces;
using NewsApp.Domain.Interfaces;
using NewsApp.Domain.Models;
using NewsApp.ViewModels;

namespace NewsApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public NewsController(INewsService newsService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var newsItems = await _newsService.GetAllAsync();
            return View(newsItems);
        }

        public async Task<IActionResult> Manage()
        {
            var newsItems = await _newsService.GetAllAsync();

            var viewModel = newsItems.Select(n => new NewsItemViewModel
            {
                Id = n.Id,
                Title = n.Title,
                Body = n.Body,
                ImageUrl = n.ImageUrl,
                CategoryId = n.CategoryId,
                CategoryName = n.category?.Title,
                CreatedAt = n.CreatedAt
            }).ToList();

            return View(viewModel);
        }


        public async Task<IActionResult> Create()
        {
            var viewModel = new NewsItemViewModel
            {
                Categories = await _categoryService.GetAllAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsItemViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            // Handle Image Upload
            string? imageUrl = null;
            if (vm.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                imageUrl = "/uploads/" + uniqueFileName;
            }

            var newsItem = new NewsItem
            {
                Title = vm.Title,
                Body = vm.Body,
                CategoryId = vm.CategoryId,
                ImageUrl = imageUrl
            };
            

            await _newsService.AddAsync(newsItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var newsItem = await _newsService.GetByIdAsync(id);
            if (newsItem == null) return NotFound();

            var vm = new NewsItemViewModel
            {
                Id = newsItem.Id,
                Title = newsItem.Title,
                Body = newsItem.Body,
                CategoryId = newsItem.CategoryId,
                ImageUrl = newsItem.ImageUrl,
                Categories = await _categoryService.GetAllAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsItemViewModel vm)
        {
            if (id != vm.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            string? imageUrl = vm.ImageUrl;
            if (vm.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                imageUrl = "/uploads/" + uniqueFileName;
            }

            var newsItem = await _newsService.GetByIdAsync(id);
            if (newsItem == null) return NotFound();

            newsItem.Title = vm.Title;
            newsItem.Body = vm.Body;
            newsItem.CategoryId = vm.CategoryId;
            newsItem.ImageUrl = imageUrl;

            await _newsService.UpdateAsync(newsItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var newsItem = await _newsService.GetByIdAsync(id);
            if (newsItem == null) return NotFound();

            return View(newsItem);
        }

        // POST: News/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsItem = await _newsService.GetByIdAsync(id);
            if (newsItem == null) return NotFound();

            await _newsService.DeleteAsync(newsItem);
            return RedirectToAction("Index");
        }
    }
}
