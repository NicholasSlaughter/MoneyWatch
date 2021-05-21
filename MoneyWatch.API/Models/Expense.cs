using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Models
{
    public class Expense
    {
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required,Range(0,1000000)]
        public decimal Price { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }

        [Required,DisplayFormat(DataFormatString = "{0:MM/dd/yy hh:mm tt}"), Display(Name = "Date of Expense")]
        public DateTimeOffset DateOfExpense { get; set; }

        [Required, Display(Name ="Category")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
