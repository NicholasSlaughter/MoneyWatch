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
    public class APIPeriodTests : Utilities.CreateRandomData
    {
        private readonly Mock<IPeriodsRepository> periodRepositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetPeriodAsync_WithUnexistingPeriod_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Set up mock repository that has no periods
                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Period)null);

                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                var result = await controller.GetPeriodAsync(Guid.NewGuid());

                //Assert
                result.Result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetPeriodAsync_WithExistingPeriod_ReturnsExpectedPeriod()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedPeriod = CreateRandomPeriod();

                //Set up mock repository that returns an expected period
                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(expectedPeriod);

                //Create an instance of the application user controller
                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetPeriodAsync(Guid.NewGuid());

                //If the result is null then exit with a fail
                if (result is null)
                    Assert.False(1 == 1);

                //Convert the result to a dto that has the values of the result
                var periodDtoResult = (result.Result as OkObjectResult).Value as PeriodDto;

                //Assert
                periodDtoResult.Should().NotBeNull();
                periodDtoResult.Should().BeEquivalentTo(
                    expectedPeriod,
                    options => options.ComparingByMembers<Period>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetPeriodsAsync_WithExistingPeriods_ReturnsExpectedPeriods()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedPeriods = new[] { CreateRandomPeriod(), CreateRandomPeriod(), CreateRandomPeriod() };

                //Set up mock repository that returns an expected period
                periodRepositoryStub.Setup(repo => repo.GetPeriodsAsync())
                    .ReturnsAsync(expectedPeriods);

                //Create an instance of the application user controller
                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetPeriodsAsync();

                //Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(
                    expectedPeriods,
                    options => options.ComparingByMembers<Period>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task CreatePeriodsAsync_WithPeriodToCreate_ReturnsCreatedPeriod()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var periodToCreate = new CreatePeriodDto(Guid.NewGuid().ToString());

                //Create an instance of the application user controller
                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.CreatePeriodAsync(periodToCreate);

                var createdPeriod = (result.Result as CreatedAtActionResult).Value as PeriodDto;

                //Assert
                periodToCreate.Should().BeEquivalentTo(
                    createdPeriod,
                    options => options.ComparingByMembers<PeriodDto>().ExcludingMissingMembers());

                //Check the data members that were not checked above
                createdPeriod.Id.Should().NotBeEmpty();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdatePeriodsAsync_WithExistingPeriod_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingPeriod = CreateRandomPeriod();

                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingPeriod);

                var periodId = existingPeriod.Id;
                var periodToUpdate = new UpdatePeriodDto(Guid.NewGuid().ToString());

                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                var result = await controller.UpdatePeriodAsync(periodId, periodToUpdate);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdatePeriodsAsync_WithUnexistingPeriod_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingPeriod = CreateRandomPeriod();

                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Period)null);

                var periodId = existingPeriod.Id;
                var periodToUpdate = new UpdatePeriodDto(Guid.NewGuid().ToString());

                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                var result = await controller.UpdatePeriodAsync(periodId, periodToUpdate);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeletePeriodsAsync_WithExistingPeriod_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingPeriod = CreateRandomPeriod();

                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingPeriod);

                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                var result = await controller.DeletePeriodAsync(existingPeriod.Id);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeletePeriodsAsync_WithUnexistingPeriod_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingPeriod = CreateRandomPeriod();

                periodRepositoryStub.Setup(repo => repo.GetPeriodAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Period)null);

                var controller = new PeriodController(periodRepositoryStub.Object);

                //Act
                var result = await controller.DeletePeriodAsync(existingPeriod.Id);

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
