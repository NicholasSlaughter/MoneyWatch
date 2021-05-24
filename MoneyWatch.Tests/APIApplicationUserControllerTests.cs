using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Controllers;
using MoneyWatch.API.Dtos;
using MoneyWatch.API.Models;
using MoneyWatch.API.Repositories;
using MoneyWatch.API.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyWatch.Tests
{
    public class APIApplicationUserControllerTests
    {
        private readonly Mock<IApplicationUsersRepository> applicationUserRepositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetApplicationUserAsync_WithUnexistingApplicationUser_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Set up mock repository that has no expenses
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync((ApplicationUser)null);

                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);


                //Act
                var result = await controller.GetApplicationUserAsync(string.Empty);

                //Assert
                result.Result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetApplicationUserAsync_WithExistingApplicationUser_ReturnsExpectedApplicationUser()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedUser = CreateRandomUser();

                //Set up mock repository that returns an expected expense
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync(expectedUser);

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetApplicationUserAsync(Guid.NewGuid().ToString());

                //If the result is null then exit with a fail
                if (result is null)
                    Assert.False(1 == 1);

                //Convert the result to a dto that has the values of the result
                var applicationUserDtoResult = (result.Result as OkObjectResult).Value as ApplicationUserDto;

                //Assert
                applicationUserDtoResult.Should().NotBeNull();
                applicationUserDtoResult.Should().BeEquivalentTo(
                    expectedUser,
                    options => options.ComparingByMembers<ApplicationUser>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetApplicationUsersAsync_WithExistingApplicationUser_ReturnsAllApplicationUser()
        {
            try
            {
                //Arrange
                //Create random users to test
                var expectedUsers = new[] { CreateRandomUser(), CreateRandomUser(), CreateRandomUser() };

                //Set up mock repository that has returns a list of users
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUsersAsync())
                    .ReturnsAsync(expectedUsers);

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //get the list of users in the database
                var result = await controller.GetApplicationUsersAsync();

                //Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(
                    expectedUsers,
                    options => options.ComparingByMembers<ApplicationUser>().ExcludingMissingMembers()); //The Identity User class has many more members than the Application User Dto does so we want to exclude those
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task CreateApplicationUsersAsync_WithApplicationUserToCreate_ReturnsCreatedApplicationUser()
        {
            try
            {
                //Arrange              
                //Create random users to test
                var expectedUsers = new CreateApplicationUserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),Guid.NewGuid().ToString(),Guid.NewGuid().ToString() + "@gmail.com");

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //get the list of users in the database
                var result = await controller.CreateApplicationUserAsync(expectedUsers,Guid.NewGuid().ToString());

                var createdApplicationUser = (result.Result as CreatedAtActionResult).Value as ApplicationUserDto;

                //Assert
                createdApplicationUser.Id.Should().NotBeNull();
                expectedUsers.Should().BeEquivalentTo(
                    createdApplicationUser,
                    options => options.ComparingByMembers<ApplicationUserDto>().ExcludingMissingMembers()); //The Identity User class has many more members than the Application User Dto does so we want to exclude those
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateApplicationUserAsync_WithExistingApplicationUser_ReturnsNoContent()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var existingUser = CreateRandomUser();

                //Set up mock repository that returns an expected expense
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync(existingUser);

                var applicationUserId = existingUser.Id;
                var userToUpdate = new UpdateApplicationUserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.UpdateApplicationUserAsync(applicationUserId,userToUpdate);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateApplicationUserAsync_WithUnexistingApplicationUser_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var existingUser = CreateRandomUser();

                //Set up mock repository that returns an expected expense
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync((ApplicationUser)null);

                var applicationUserId = existingUser.Id;
                var userToUpdate = new UpdateApplicationUserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.UpdateApplicationUserAsync(applicationUserId, userToUpdate);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteApplicationUserAsync_WithExistingApplicationUser_ReturnsNoContent()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var existingUser = CreateRandomUser();

                //Set up mock repository that returns an expected expense
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync(existingUser);

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.DeleteApplicationUserAsync(existingUser.Id);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteApplicationUserAsync_WithUnexistingApplicationUser_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var existingUser = CreateRandomUser();

                //Set up mock repository that returns an expected expense
                applicationUserRepositoryStub.Setup(repo => repo.GetApplicationUserAsync(It.IsAny<string>()))
                    .ReturnsAsync((ApplicationUser)null);

                //Create an instance of the application user controller
                var controller = new ApplicationUserController(applicationUserRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.DeleteApplicationUserAsync(existingUser.Id);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        private ApplicationUser CreateRandomUser()
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

        private Expense CreateRandomExpense(string userId)
        {
            var category = CreateRandomCategory();
            return new()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                Price = rand.Next(1,1000000),
                Description = Guid.NewGuid().ToString(),
                DateOfExpense = DateTimeOffset.UtcNow,
                CategoryId = category.Id.ToString()
            };
        }

        private Alert CreateRandomAlert(string userId)
        {
            var category = CreateRandomCategory();
            var period = CreateRandomPeriod();
            return new()
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                Price = rand.Next(1, 1000000),
                AlertCreationDate = DateTimeOffset.UtcNow,
                PeriodStartDate = DateTimeOffset.UtcNow,
                PriodEndDate = DateTimeOffset.UtcNow.AddDays(rand.Next(1, 7)),
                PeriodId = period.Id.ToString(),
                CategoryId = category.Id.ToString()
            };
        }

        private Period CreateRandomPeriod() => new() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };

        private Category CreateRandomCategory() => new() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
    }
}
