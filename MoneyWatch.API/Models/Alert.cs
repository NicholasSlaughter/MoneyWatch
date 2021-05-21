using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Models
{
    public class Alert
    {
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required, Range(0,1000000)]
        public decimal Price { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Alert Creation")]
        public DateTimeOffset AlertCreationDate { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Period Start Date")]
        public DateTimeOffset PeriodStartDate { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Period End Date")]
        public DateTimeOffset PriodEndDate { get; set; }

        [Display(Name ="Period")]
        public string PeriodId { get; set; }
        public Period Period { get; set; }
        [Display(Name ="Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
