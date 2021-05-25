using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public interface IExpensesRepository
    {
        public Task CreateExpenseAsync(Expense expense);
        public Task<Expense> GetExpenseAsync(Guid id);
        public Task<IEnumerable<Expense>> GetExpensesAsync();
        public Task UpdateExpenseAsync(Expense expense);
        public Task DeleteExpenseAsync(Guid id);
    }
}
