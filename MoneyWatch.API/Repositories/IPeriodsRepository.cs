using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public interface IPeriodsRepository
    {
        public Task CreatePeriodAsync(Period period);
        public Task<Period> GetPeriodAsync(Guid id);
        public Task<IEnumerable<Period>> GetPeriodsAsync();
        public Task UpdatePeriodAsync(Period period);
        public Task DeletePeriodAsync(Guid id);
    }
}
