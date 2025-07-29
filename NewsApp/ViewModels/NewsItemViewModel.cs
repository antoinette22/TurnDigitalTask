using NewsApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
    public class NewsItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }    
    }
}
