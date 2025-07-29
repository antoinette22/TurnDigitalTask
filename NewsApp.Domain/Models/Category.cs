namespace NewsApp.Domain.Models
{
    public class Category : BaseEntity
    {

        public string? Description { get; set; }
        public string Slug { get; set; } = string.Empty;
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

        public ICollection<NewsItem> NewsItems { get; set; }
                                = new HashSet<NewsItem>();
    }
}
