using MoneyWatch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWatch.Tests.Utilities
{
    public class CreateRandomData
    {
        private readonly Random rand = new();
        protected ApplicationUser CreateRandomUser()
        {
            var idToPass = Guid.NewGuid().ToString();
            return new()
            {
                Id = idToPass,
                First_Name = Guid.NewGuid().ToString(),
                Last_Name = Guid.NewGuid().ToString(),
                Expenses =
                {
                    CreateRandomExpense(idToPass),
                    CreateRandomExpense(idToPass),
                    CreateRandomExpense(idToPass)
                },
                Alerts =
                {
                    CreateRandomAlert(idToPass),
                    CreateRandomAlert(idToPass),
                    CreateRandomAlert(idToPass)
                }
            };
        }

        protected Expense CreateRandomExpense(string userId = "default")
        {
            var category = CreateRandomCategory();

            if (userId.Equals("default"))
                userId = Guid.NewGuid().ToString();

            return new()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                Price = rand.Next(1, 1000000),
                Description = Guid.NewGuid().ToString(),
                DateOfExpense = DateTimeOffset.UtcNow,
                CategoryId = category.Id.ToString(),
                Category = category
            };
        }

        protected Alert CreateRandomAlert(string userId = "default")
        {
            var category = CreateRandomCategory();
            var period = CreateRandomPeriod();

            if (userId.Equals("default"))
                userId = Guid.NewGuid().ToString();

            return new()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                Price = rand.Next(1, 1000000),
                AlertCreationDate = DateTimeOffset.UtcNow,
                PeriodStartDate = DateTimeOffset.UtcNow,
                PeriodEndDate = DateTimeOffset.UtcNow.AddDays(rand.Next(1, 7)),
                PeriodId = period.Id.ToString(),
                Period = period,
                CategoryId = category.Id.ToString(),
                Category = category
            };
        }

        protected Period CreateRandomPeriod()
        {
            var randNumber = rand.Next(1, 3);

            if (randNumber.Equals(1))
                return new() { Id = Guid.NewGuid(), Name = "Week" };
            if(randNumber.Equals(2))
                return new() { Id = Guid.NewGuid(), Name = "Month" };

            return new() { Id = Guid.NewGuid(), Name = "Year" };
        }

        protected Category CreateRandomCategory() => new() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
    }
}
