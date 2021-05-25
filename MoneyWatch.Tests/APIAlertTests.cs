using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Controllers;
using MoneyWatch.API.Dtos;
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
    public class APIAlertTests : Utilities.CreateRandomData
    {
        private readonly Mock<IAlertsRepository> alertRepositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetAlertAsync_WithUnexistingAlert_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Set up mock repository that has no Alerts
                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Alert)null);

                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                var result = await controller.GetAlertAsync(Guid.NewGuid());

                //Assert
                result.Result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetAlertAsync_WithExistingAlert_ReturnsExpectedAlert()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedAlert = CreateRandomAlert();

                //Set up mock repository that returns an expected Alert
                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(expectedAlert);

                //Create an instance of the application user controller
                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetAlertAsync(Guid.NewGuid());

                //If the result is null then exit with a fail
                if (result is null)
                    Assert.False(1 == 1);

                //Convert the result to a dto that has the values of the result
                var alertDtoResult = (result.Result as OkObjectResult).Value as AlertDto;

                //Assert
                alertDtoResult.Should().NotBeNull();
                alertDtoResult.Should().BeEquivalentTo(
                    expectedAlert,
                    options => options.ComparingByMembers<Alert>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetAlertsAsync_WithExistingAlerts_ReturnsExpectedAlerts()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedAlerts = new[] { CreateRandomAlert(), CreateRandomAlert(), CreateRandomAlert() };

                //Set up mock repository that returns an expected Alert
                alertRepositoryStub.Setup(repo => repo.GetAlertsAsync())
                    .ReturnsAsync(expectedAlerts);

                //Create an instance of the application user controller
                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetAlertsAsync();

                //Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(
                    expectedAlerts,
                    options => options.ComparingByMembers<Alert>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task CreateAlertsAsync_WithAlertToCreate_ReturnsCreatedAlert()
        {
            try
            {
                //Arrange
                double daysToAdd = 7; //Default add day to 7 days (weekly period)

                var randomPeriod = CreateRandomPeriod();
                var randomCategory = CreateRandomCategory();

                //Create a random user to test
                var alertToCreate = new CreateAlertDto(Guid.NewGuid().ToString(), rand.Next(1, 1000000), Guid.NewGuid().ToString(), randomPeriod, Guid.NewGuid().ToString(), randomCategory);

                //Create an instance of the application user controller
                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.CreateAlertAsync(alertToCreate);

                var createdAlert = (result.Result as CreatedAtActionResult).Value as AlertDto;

                if(randomPeriod.Name.Equals("Month"))
                    daysToAdd = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                if(randomPeriod.Name.Equals("Year"))
                    daysToAdd = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

                //Assert
                alertToCreate.Should().BeEquivalentTo(
                    createdAlert,
                    options => options.ComparingByMembers<AlertDto>().ExcludingMissingMembers());

                //Check the data members that were not checked above
                createdAlert.Id.Should().NotBeEmpty();
                createdAlert.AlertCreationDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
                createdAlert.PeriodStartDate.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000);
                createdAlert.PeriodEndDate.Should().BeCloseTo(DateTimeOffset.UtcNow.AddDays(daysToAdd), 1000);
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateAlertsAsync_WithExistingAlert_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingAlert = CreateRandomAlert();

                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingAlert);

                var alertId = existingAlert.Id;
                var alertToUpdate = new UpdateAlertDto(existingAlert.Price + 5);

                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                var result = await controller.UpdateAlertAsync(alertId, alertToUpdate);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateAlertsAsync_WithUnexistingAlert_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingAlert = CreateRandomAlert();

                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Alert)null);

                var alertId = existingAlert.Id;
                var alertToUpdate = new UpdateAlertDto(existingAlert.Price + 5);

                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                var result = await controller.UpdateAlertAsync(alertId, alertToUpdate);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteAlertsAsync_WithExistingAlert_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingAlert = CreateRandomAlert();

                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingAlert);

                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                var result = await controller.DeleteAlertAsync(existingAlert.Id);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteAlertsAsync_WithUnexistingAlert_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingAlert = CreateRandomAlert();

                alertRepositoryStub.Setup(repo => repo.GetAlertAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Alert)null);

                var controller = new AlertController(alertRepositoryStub.Object);

                //Act
                var result = await controller.DeleteAlertAsync(existingAlert.Id);

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
