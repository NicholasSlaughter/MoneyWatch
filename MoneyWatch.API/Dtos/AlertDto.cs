using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Dtos
{
    public record AlertDto(
        Guid Id,
        [Required, Display(Name = "Application User")]
        string ApplicationUserId,
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price,
        [Required, Display(Name = "Alert Creation")]
        DateTimeOffset AlertCreationDate,
        [Required, Display(Name = "Period Start Date")]
        DateTimeOffset PeriodStartDate,
        [Required, Display(Name = "Period End Date")]
        DateTimeOffset PeriodEndDate,
        [Display(Name ="Period")]
        string PeriodId,
        Period Period,
        [Required, Display(Name = "Category")]
        string CategoryId,
        Category Category);

    public record CreateAlertDto(
        [Required, Display(Name = "Application User")]
        string ApplicationUserId,
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price,
        [Display(Name ="Period")]
        string PeriodId,
        Period Period,
        [Required, Display(Name = "Category")]
        string CategoryId,
        Category Category);

    public record UpdateAlertDto(
        [Required, Display(Name = "Price"), Range(1,1000000)]
        decimal Price);
}
