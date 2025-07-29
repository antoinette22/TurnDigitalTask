using Microsoft.EntityFrameworkCore;
using NewsApp.ApplicationLayer.Interfaces;
using NewsApp.Domain.Interfaces;
using NewsApp.Domain.Models;
using NewsApp.DTOs;
using System.Linq.Expressions;

namespace NewsApp.DataAccessLayer.Services.NewsSErvice
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }
        public async Task<IEnumerable<NewsItem>> GetAllAsync()
        {
            return await _newsRepository.GetAllAsync(
                include: query => query.Include(n => n.category)
            );
        }

        // Get single news item including Category
        public async Task<NewsItem?> GetByIdAsync(int id)
        {
            return await _newsRepository.FindAsync(
                n => n.Id == id,
                include: query => query.Include(n => n.category)
            );
        }

        public async Task AddAsync(NewsItem newsItem) =>
            await _newsRepository.AddAsync(newsItem);

        public async Task UpdateAsync(NewsItem newsItem) =>
            await _newsRepository.Update(newsItem);

        public async Task DeleteAsync(NewsItem newsItem) =>
            await _newsRepository.Delete(newsItem);

        public async Task<NewsItem?> FindAsync(Expression<Func<NewsItem, bool>> predicate) =>
            await _newsRepository.FindAsync(predicate);

        public async Task<NewsToReturnDto?> GetBySlugsAsync(string categorySlug, string newsSlug)
        {
            var result = await _newsRepository.FindAsync(
                n => n.Slug == newsSlug && n.category.Slug == categorySlug,
                include: q => q.Include(n => n.category)
            );
            return new NewsToReturnDto
            {
                Title = result.Title,
                Body = result.Body,
                CreatedAt = result.CreatedAt,
                ImageUrl = result.ImageUrl,
                CategoryName = result.category.Title
            };
        }
        public async Task<IEnumerable<NewsItem>> GetByCategoryAsync(string categorySlug)
        {
            return await _newsRepository.GetAllAsync(
                filter: n => n.category.Slug == categorySlug,
                include: q => q.Include(n => n.category)
            );
        }

        public async Task<(IEnumerable<NewsToReturnDto> Items, int TotalCount)> GetPaginatedNewsAsync(int pageIndex, int pageSize, string? CategorySlug = null)
        {
            var (news, total) = await _newsRepository.GetPaginatedNewsAsync(pageIndex, pageSize, CategorySlug);

            var newsDtos = news.Select(n => new NewsToReturnDto
            {
                Title = n.Title,
                Body = n.Body,
                CreatedAt = n.CreatedAt,
                ImageUrl = n.ImageUrl,
                CategoryName = n.category.Title
            });
            return (newsDtos, total);
        }

    }
}

