using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Domain.Interfaces;
using NewsApp.Domain.Models;

namespace NewsApp.DataAccessLayer.Data.Repositories
{
    public class NewsRepository : GenericRepository<NewsItem>, INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<NewsItem?> GetNewsByIdWithCategoryAsync(int id)
        {

            return await _context.NewsItems
                .Include(n => n.category)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<NewsItem>> GetNewsWithCategoryAsync()
        {
            return await _context.NewsItems
             .Include(n => n.category)
             .OrderByDescending(n => n.CreatedAt)
             .ToListAsync();
        }
        public async Task<NewsItem?> GetNewsBySlugAsync(string slug)
        {
            return await _context.NewsItems
                .Include(n => n.category)
                .FirstOrDefaultAsync(n => n.Slug == slug);
        }
        public async Task<(IEnumerable<NewsItem> Items, int TotalCount)> GetPaginatedNewsAsync(int pageIndex, int pageSize, string? CategorySlug = null)
        {
            var query = _context.NewsItems
                .Include(n => n.category)
                .AsQueryable();

            if (!CategorySlug.IsNullOrEmpty())
            {
                query = query.Where(n => n.category.Slug == CategorySlug);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
