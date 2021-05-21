using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace MoneyWatch.Tests
{
    public class APIExpensesTests
    {
        //private readonly Mock<IExpensesRepository> repositoryStub = new();

        [Fact]
        public void GetExpenseAsync_WithUnexistingExpense_ReturnsNotFound()
        {
            //try
            //{
            //    //Arrange
            //    //Set up mock repository that has no expenses
            //    repositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
            //        .ReturnsAsync((Expense)null);

            //    var controller = new ExpenseController(repository.Object);


            //    //Act
            //    var result = await controller.GetExpenseAsync(Guid.NewGuid());

            //    //Assert
            //    result.Result.Should().BeOfType<NotFoundResult>();
            //}
            //catch(Exception e)
            //{
            //    Assert.False(1 == 1);
            //}
        }
    }
}
