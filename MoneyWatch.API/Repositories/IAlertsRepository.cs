using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public interface IAlertsRepository
    {
        public Task CreateAlertAsync(Alert alert);
        public Task<Alert> GetAlertAsync(Guid id);
        public Task<IEnumerable<Alert>> GetAlertsAsync();
        public Task UpdateAlertAsync(Alert alert);
        public Task DeleteAlertAsync(Guid id);
    }
}
