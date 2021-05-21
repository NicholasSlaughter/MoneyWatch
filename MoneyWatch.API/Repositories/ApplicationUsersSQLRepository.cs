using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class ApplicationUsersSQLRepository : IApplicationUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUsersSQLRepository(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task CreateApplicationUserAsync(ApplicationUser user, string password) => await _userManager.CreateAsync(user,password);
        public async Task<ApplicationUser> GetApplicationUserAsync(string id) => await _userManager.FindByIdAsync(id);
        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync() => await _userManager.Users.ToListAsync();
        public async Task UpdateApplicationUserAsync(ApplicationUser user) => await _userManager.UpdateAsync(user);
        public async Task DeleteApplicationUserAsync(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            await _userManager.DeleteAsync(userToDelete);
        }
    }
}
