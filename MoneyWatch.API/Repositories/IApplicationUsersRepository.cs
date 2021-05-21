using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public interface IApplicationUsersRepository
    {
        public Task CreateApplicationUserAsync(ApplicationUser user, string password);
        public Task<ApplicationUser> GetApplicationUserAsync(string id);
        public Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync();
        public Task UpdateApplicationUserAsync(ApplicationUser user);
        public Task DeleteApplicationUserAsync(string id);
    }
}
