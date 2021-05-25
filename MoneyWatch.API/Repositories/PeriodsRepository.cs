using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class PeriodsRepository : IPeriodsRepository
    {
        private readonly ApplicationDbContext _context;

        public PeriodsRepository(ApplicationDbContext context) => _context = context;
        public async Task CreatePeriodAsync(Period period)
        {
            _context.Period.Add(period);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePeriodAsync(Guid id)
        {
            var PeriodToDelete = await _context.Period.FindAsync(id);

            _context.Period.Remove(PeriodToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Period> GetPeriodAsync(Guid id) => await _context.Period.FindAsync(id);

        public async Task<IEnumerable<Period>> GetPeriodsAsync() => await _context.Period.ToListAsync();

        public async Task UpdatePeriodAsync(Period period)
        {
            _context.Period.Update(period);

            await _context.SaveChangesAsync();
        }
    }
}
