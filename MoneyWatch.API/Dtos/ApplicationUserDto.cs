using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Dtos
{
    public record ApplicationUserDto(
        [Required, Display(Name = "First Name"), MaxLength(50)]
        string First_Name,
        [Required, Display(Name = "Last Name"), MaxLength(50)]
        string Last_Name,
        ICollection<Expense> Expenses,
        ICollection<Alert> Alerts);

    public record CreateApplicationUserDto(
        [Required, Display(Name = "First Name"), MaxLength(50)]
        string First_Name,
        [Required, Display(Name = "Last Name"), MaxLength(50)]
        string Last_Name,
        ICollection<Expense> Expenses,
        ICollection<Alert> Alerts);

    public record UpdateApplicationUserDto(
        [Required, Display(Name = "First Name"), MaxLength(50)]
        string First_Name,
        [Required, Display(Name = "Last Name"), MaxLength(50)]
        string Last_Name,
        ICollection<Expense> Expenses,
        ICollection<Alert> Alerts);
}
