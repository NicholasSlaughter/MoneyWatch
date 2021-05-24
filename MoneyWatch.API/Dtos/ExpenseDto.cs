using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Dtos
{
    public record ExpenseDto(
        Guid Id,
        [Required, Display(Name = "Application User")]
        string ApplicationUserId,
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price,
        [Required, Display(Name = "Description"), MaxLength(255)]
        string Description,
        [Required, Display(Name = "Date of Expense")]
        DateTimeOffset DateOfExpense,
        [Required, Display(Name = "Category")]
        string CategoryId);

    public record CreateExpenseDto(
        [Required, Display(Name = "Application User")]
        string ApplicationUserId,
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price,
        [Required, Display(Name = "Description"), MaxLength(255)]
        string Description,
        [Required, Display(Name = "Category")]
        string CategoryId);

    public record UpdateExpenseDto(
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price,
        [Required, Display(Name = "Description"), MaxLength(255)]
        string Description,
        [Required, Display(Name = "Category")]
        string CategoryId);
}
