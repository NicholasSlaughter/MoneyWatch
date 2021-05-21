using MoneyWatch.API.Dtos;
using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Utilities
{
    public static class DtoExtensions
    {
        public static ApplicationUserDto AsDto(this ApplicationUser user) => new ApplicationUserDto(user.First_Name, user.Last_Name, user.Expenses, user.Alerts);
    }
}
