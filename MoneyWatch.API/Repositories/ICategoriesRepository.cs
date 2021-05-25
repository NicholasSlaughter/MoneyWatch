using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public interface ICategoriesRepository
    {
        public Task CreateCategoryAsync(Category category);
        public Task<Category> GetCategoryAsync(Guid id);
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task UpdateCategoryAsync(Category category);
        public Task DeleteCategoryAsync(Guid id);
    }
}
