using MoneyWatch.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Utilities
{
    public class ValidDataCheck
    {
        private readonly ApplicationDbContext _context;

        public ValidDataCheck(ApplicationDbContext context) => _context = context;

        public async Task<bool> UserExistsAsync(string id)
        {
            var existingUser = await _context.Users.FindAsync(id);

            return existingUser is not null;
        }
        public async Task<bool> ExpenseExistsAsync(string id)
        {
            var existingExpense = await _context.Expense.FindAsync(id);

            return existingExpense is not null;
        }

        public async Task<bool> AlertExistsAsync(string id)
        {
            var existingAlert = await _context.Alert.FindAsync(id);

            return existingAlert is not null;
        }

        public async Task<bool> PeriodExistsAsync(string id)
        {
            var existingPeriod = await _context.Period.FindAsync(id);

            return existingPeriod is not null;
        }

        public async Task<bool> CategoryExistsAsync(string id)
        {
            var existingCategory = await _context.Category.FindAsync(id);

            return existingCategory is not null;
        }
    }
}
