using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpensesRepository(ApplicationDbContext context) => _context = context;
        public async Task CreateExpenseAsync(Expense expense)
        {
            _context.Expense.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            var expenseToDelete = await _context.Expense.FindAsync(id);

            _context.Expense.Remove(expenseToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Expense> GetExpenseAsync(Guid id) => await _context.Expense.FindAsync(id);

        public async Task<IEnumerable<Expense>> GetExpensesAsync() => await _context.Expense.ToListAsync();

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Expense.Update(expense);

            await _context.SaveChangesAsync();
        }
    }
}
