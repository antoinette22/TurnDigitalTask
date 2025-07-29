namespace NewsApp.Domain.Models
{
    public class NewsItem : BaseEntity
    {
        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Slug = GenerateSlug(value);
            }
        }

        private string GenerateSlug(string title)
        {
            return title
                .ToLower()
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("?", "")
                .Replace("!", "");
        }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public string Slug { get; set; } = string.Empty;
    }
}
