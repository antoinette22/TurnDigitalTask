namespace NewsApp.DTOs
{
    public class NewsToReturnDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
