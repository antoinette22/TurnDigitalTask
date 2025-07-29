using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.ApplicationLayer.Interfaces;
using NewsApp.Domain.Interfaces;
using NewsApp.Models;
using NewsApp.ViewModels;
using System.Diagnostics;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public HomeController(INewsService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? categorySlug)
        {
            var allCategories = await _categoryService.GetAllAsync();

            var news = string.IsNullOrEmpty(categorySlug)
                ? await _newsService.GetAllAsync()
                : await _newsService.GetByCategoryAsync(categorySlug);

            var vm = new NewsListingViewModel
            {
                News = news,
                Categories = allCategories.Select(c => new SelectListItem
                {
                    Value = c.Slug,
                    Text = c.Title
                }).ToList(),
                SelectedCategorySlug = categorySlug
            };

            return View(vm);
        }
        public async Task<IActionResult> Details(string categorySlug, string newsSlug)
        {
            if (string.IsNullOrEmpty(categorySlug) || string.IsNullOrEmpty(newsSlug))
                return NotFound();

            var newsItem = await _newsService.GetBySlugsAsync(categorySlug, newsSlug);

            if (newsItem == null)
                return NotFound();

            return View(newsItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
