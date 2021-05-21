using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Models
{
    public class Period
    {
        public Guid Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}
