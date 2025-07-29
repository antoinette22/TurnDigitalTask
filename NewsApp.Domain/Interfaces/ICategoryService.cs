using NewsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Domain.Interfaces
{
    public interface ICategoryService 
    {

        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetByTitleAsync(string title);
        Task AddAsync(Category category);
        Task Update(Category category);
        Task Delete(Category entity);
        Task<bool> ExistsAsync(int id);
    }
}
