using NewsApp.Domain.Models;
using NewsApp.DTOs;
using System.Linq.Expressions;

namespace NewsApp.ApplicationLayer.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsItem>> GetAllAsync();
        Task<NewsItem?> GetByIdAsync(int id);
        Task AddAsync(NewsItem newsItem);
        Task UpdateAsync(NewsItem newsItem);
        Task DeleteAsync(NewsItem newsItem);
        Task<NewsItem?> FindAsync(Expression<Func<NewsItem, bool>> predicate);
        Task<NewsToReturnDto?> GetBySlugsAsync(string categorySlug, string newsSlug);
        Task<IEnumerable<NewsItem>> GetByCategoryAsync(string CategorySlug);
        Task<(IEnumerable<NewsToReturnDto> Items, int TotalCount)> GetPaginatedNewsAsync(int pageIndex, int pageSize, string? CategorySlug = null);
    }
}
