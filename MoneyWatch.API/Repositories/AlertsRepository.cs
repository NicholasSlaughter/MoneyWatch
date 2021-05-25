using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class AlertsRepository : IAlertsRepository
    {
        private readonly ApplicationDbContext _context;

        public AlertsRepository(ApplicationDbContext context) => _context = context;
        public async Task CreateAlertAsync(Alert alert)
        {
            _context.Alert.Add(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlertAsync(Guid id)
        {
            var AlertToDelete = await _context.Alert.FindAsync(id);

            _context.Alert.Remove(AlertToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Alert> GetAlertAsync(Guid id) => await _context.Alert.FindAsync(id);

        public async Task<IEnumerable<Alert>> GetAlertsAsync() => await _context.Alert.ToListAsync();

        public async Task UpdateAlertAsync(Alert alert)
        {
            _context.Alert.Update(alert);

            await _context.SaveChangesAsync();
        }
    }
}
