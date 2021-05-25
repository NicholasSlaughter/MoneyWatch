using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Dtos
{
    public record CategoryDto(
        Guid Id,
        [Required, MaxLength(255)]
        string Name);

    public record CreateCategoryDto(
        [Required, MaxLength(255)]
        string Name);

    public record UpdateCategoryDto(
        [Required, MaxLength(255)]
        string Name);
}
