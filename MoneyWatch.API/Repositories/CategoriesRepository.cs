using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context) => _context = context;
        public async Task CreateCategoryAsync(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var CategoryToDelete = await _context.Category.FindAsync(id);

            _context.Category.Remove(CategoryToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid id) => await _context.Category.FindAsync(id);

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Category.ToListAsync();

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Category.Update(category);

            await _context.SaveChangesAsync();
        }
    }
}
