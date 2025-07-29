using NewsApp.Domain.Models;

namespace NewsApp.Domain.Interfaces
{
    public interface INewsRepository : IGenericRepository<NewsItem>
    {
        Task<IEnumerable<NewsItem>> GetNewsWithCategoryAsync();
        Task<NewsItem?> GetNewsByIdWithCategoryAsync(int id);
        Task<(IEnumerable<NewsItem> Items, int TotalCount)> GetPaginatedNewsAsync(int pageIndex, int pageSize, string? CategorySlug = null);


    }
}
