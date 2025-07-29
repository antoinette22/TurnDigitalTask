using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Domain.Models;

namespace NewsApp.ViewModels
{
    public class NewsListingViewModel
    {

        //public IEnumerable<NewsItem> News { get; set; } = Enumerable.Empty<NewsItem>();
        ////public SelectList Categories { get; set; } = null!;
        //public string? SelectedCategorySlug { get; set; }

        public IEnumerable<NewsItem> News { get; set; } = Enumerable.Empty<NewsItem>();
        public List<SelectListItem> Categories { get; set; } = new();
        public string? SelectedCategorySlug { get; set; }
    }
}
