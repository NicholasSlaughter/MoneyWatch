using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Controllers;
using MoneyWatch.API.Models;
using MoneyWatch.API.Repositories;
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
    }
}
