using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyWatch.API.Data;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyWatch.API.Repositories
{
    public class ApplicationUsersSQLRepository : IApplicationUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUsersSQLRepository(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IdentityResult> CreateApplicationUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypes.Role, "User"));
            return result;
        }
        public async Task<ApplicationUser> GetApplicationUserAsync(string id) => await _userManager.FindByIdAsync(id);
        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync() => await _userManager.Users.ToListAsync();
        public async Task UpdateApplicationUserAsync(ApplicationUser user, IEnumerable<Claim> newClaims = null)
        {
            await _userManager.UpdateAsync(user);
            if (newClaims is null)
            {
                var updatedUser = await _userManager.FindByIdAsync(user.Id);
                await _userManager.RemoveClaimsAsync(updatedUser, (IEnumerable<Claim>)_userManager.GetClaimsAsync(updatedUser));
                foreach (var claim in newClaims)
                    await _userManager.AddClaimAsync(updatedUser, claim);
            }
        }
        public async Task DeleteApplicationUserAsync(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);

            await _userManager.DeleteAsync(userToDelete);
        }
    }
}
