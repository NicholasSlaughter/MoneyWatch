using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyWatch.API.Controllers;
using MoneyWatch.API.Data;
using MoneyWatch.API.Dtos;
using MoneyWatch.API.Models;
using MoneyWatch.API.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MoneyWatch.Tests
{
    public class APIExpensesTests : Utilities.CreateRandomData
    {
        private readonly Mock<IExpensesRepository> expenseRepositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetExpenseAsync_WithUnexistingExpense_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Set up mock repository that has no expenses
                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Expense)null);

                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                var result = await controller.GetExpenseAsync(Guid.NewGuid());

                //Assert
                result.Result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetExpenseAsync_WithExistingExpense_ReturnsExpectedExpense()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedExpense = CreateRandomExpense();

                //Set up mock repository that returns an expected expense
                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(expectedExpense);

                //Create an instance of the application user controller
                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetExpenseAsync(Guid.NewGuid());

                //If the result is null then exit with a fail
                if (result is null)
                    Assert.False(1 == 1);

                //Convert the result to a dto that has the values of the result
                var expenseDtoResult = (result.Result as OkObjectResult).Value as ExpenseDto;

                //Assert
                expenseDtoResult.Should().NotBeNull();
                expenseDtoResult.Should().BeEquivalentTo(
                    expectedExpense,
                    options => options.ComparingByMembers<Expense>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetExpensesAsync_WithExistingExpenses_ReturnsExpectedExpenses()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedExpenses = new[] { CreateRandomExpense(), CreateRandomExpense(), CreateRandomExpense() };

                //Set up mock repository that returns an expected expense
                expenseRepositoryStub.Setup(repo => repo.GetExpensesAsync())
                    .ReturnsAsync(expectedExpenses);

                //Create an instance of the application user controller
                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetExpensesAsync();

                //Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(
                    expectedExpenses,
                    options => options.ComparingByMembers<Expense>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task CreateExpensesAsync_WithExpenseToCreate_ReturnsCreatedExpense()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expenseToCreate = new CreateExpenseDto(Guid.NewGuid().ToString(),rand.Next(1,1000000),Guid.NewGuid().ToString(),Guid.NewGuid().ToString());

                //Create an instance of the application user controller
                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.CreateExpenseAsync(expenseToCreate);

                var createdExpense = (result.Result as CreatedAtActionResult).Value as ExpenseDto;

                //Assert
                expenseToCreate.Should().BeEquivalentTo(
                    createdExpense,
                    options => options.ComparingByMembers<ExpenseDto>().ExcludingMissingMembers());

                //Check the data members that were not checked above
                createdExpense.Id.Should().NotBeEmpty();
                createdExpense.DateOfExpense.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateExpensesAsync_WithExistingExpense_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingExpense = CreateRandomExpense();

                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingExpense);

                var expenseId = existingExpense.Id;
                var expenseToUpdate = new UpdateExpenseDto(existingExpense.Price + 5, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                var result = await controller.UpdateExpenseAsync(expenseId, expenseToUpdate);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateExpensesAsync_WithUnexistingExpense_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingExpense = CreateRandomExpense();

                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Expense)null);

                var expenseId = existingExpense.Id;
                var expenseToUpdate = new UpdateExpenseDto(existingExpense.Price + 5, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                var result = await controller.UpdateExpenseAsync(expenseId, expenseToUpdate);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteExpensesAsync_WithExistingExpense_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingExpense = CreateRandomExpense();

                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingExpense);

                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                var result = await controller.DeleteExpenseAsync(existingExpense.Id);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteExpensesAsync_WithUnexistingExpense_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingExpense = CreateRandomExpense();

                expenseRepositoryStub.Setup(repo => repo.GetExpenseAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Expense)null);

                var controller = new ExpenseController(expenseRepositoryStub.Object);

                //Act
                var result = await controller.DeleteExpenseAsync(existingExpense.Id);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }
    }
}
