using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Dtos
{
    public record PeriodDto(
        Guid Id,
        [Required, MaxLength(255)]
        string Name);

    public record CreatePeriodDto(
        [Required, MaxLength(255)]
        string Name);

    public record UpdatePeriodDto(
        [Required, MaxLength(255)]
        string Name);
}
