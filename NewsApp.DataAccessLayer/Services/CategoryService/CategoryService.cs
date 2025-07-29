using NewsApp.Domain.Interfaces;
using NewsApp.Domain.Models;

namespace NewsApp.DataAccessLayer.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Category category)
        {

            var existing = await _categoryRepository.FindAsync(c => c.Title == category.Title);
            if (existing != null)
                throw new Exception("Title must be unique");

            await _categoryRepository.AddAsync(category);
        }

        public async Task Delete(Category entity)
        {
            var existing = await _categoryRepository.FindAsync(e => e.Id == entity.Id);
            if (existing == null)
                throw new Exception("Category not found");

            await Task.Run(() => _categoryRepository.Delete(existing));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _categoryRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync(null);
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.FindAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByTitleAsync(string title)
        {
            return await _categoryRepository.FindAsync(c => c.Title == title);
        }

        public async Task Update(Category category)
        {
            // Unique Title Check excluding current id
            var existing = await _categoryRepository.FindAsync(
                c => c.Title == category.Title && c.Id != category.Id);

            if (existing != null)
                throw new Exception("Title must be unique");

            await Task.Run(() => _categoryRepository.Update(category));
        }
    }
}
