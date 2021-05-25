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
    public class APICategoryTests :Utilities.CreateRandomData
    {
        private readonly Mock<ICategoriesRepository> categoryRepositoryStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetCategoryAsync_WithUnexistingCategory_ReturnsNotFound()
        {
            try
            {
                //Arrange
                //Set up mock repository that has no categories
                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Category)null);

                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                var result = await controller.GetCategoryAsync(Guid.NewGuid());

                //Assert
                result.Result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetCategoryAsync_WithExistingCategory_ReturnsExpectedCategory()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedCategory = CreateRandomCategory();

                //Set up mock repository that returns an expected category
                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(expectedCategory);

                //Create an instance of the application user controller
                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetCategoryAsync(Guid.NewGuid());

                //If the result is null then exit with a fail
                if (result is null)
                    Assert.False(1 == 1);

                //Convert the result to a dto that has the values of the result
                var categoryDtoResult = (result.Result as OkObjectResult).Value as CategoryDto;

                //Assert
                categoryDtoResult.Should().NotBeNull();
                categoryDtoResult.Should().BeEquivalentTo(
                    expectedCategory,
                    options => options.ComparingByMembers<Category>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task GetCategoriesAsync_WithExistingCategories_ReturnsExpectedCategories()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var expectedCategories = new[] { CreateRandomCategory(), CreateRandomCategory(), CreateRandomCategory() };

                //Set up mock repository that returns an expected category
                categoryRepositoryStub.Setup(repo => repo.GetCategoriesAsync())
                    .ReturnsAsync(expectedCategories);

                //Create an instance of the application user controller
                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.GetCategoriesAsync();

                //Assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(
                    expectedCategories,
                    options => options.ComparingByMembers<Category>().ExcludingMissingMembers());
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task CreateCategoriesAsync_WithCategoryToCreate_ReturnsCreatedCategory()
        {
            try
            {
                //Arrange
                //Create a random user to test
                var categoryToCreate = new CreateCategoryDto(Guid.NewGuid().ToString());

                //Create an instance of the application user controller
                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                //Get the application user from the database
                var result = await controller.CreateCategoryAsync(categoryToCreate);

                var createdCategory = (result.Result as CreatedAtActionResult).Value as CategoryDto;

                //Assert
                categoryToCreate.Should().BeEquivalentTo(
                    createdCategory,
                    options => options.ComparingByMembers<CategoryDto>().ExcludingMissingMembers());

                //Check the data members that were not checked above
                createdCategory.Id.Should().NotBeEmpty();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateCategoriesAsync_WithExistingCategory_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingCategory = CreateRandomCategory();

                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingCategory);

                var categoryId = existingCategory.Id;
                var categoryToUpdate = new UpdateCategoryDto(Guid.NewGuid().ToString());

                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                var result = await controller.UpdateCategoryAsync(categoryId, categoryToUpdate);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task UpdateCategoriesAsync_WithUnexistingCategory_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingCategory = CreateRandomCategory();

                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Category)null);

                var categoryId = existingCategory.Id;
                var categoryToUpdate = new UpdateCategoryDto(Guid.NewGuid().ToString());

                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                var result = await controller.UpdateCategoryAsync(categoryId, categoryToUpdate);

                //Assert
                result.Should().BeOfType<NotFoundResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteCategoriesAsync_WithExistingCategory_ReturnsNoContent()
        {
            try
            {
                //Arrange
                var existingCategory = CreateRandomCategory();

                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(existingCategory);

                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                var result = await controller.DeleteCategoryAsync(existingCategory.Id);

                //Assert
                result.Should().BeOfType<NoContentResult>();
            }
            catch (Exception e)
            {
                Assert.False(1 == 1);
            }
        }

        [Fact]
        public async Task DeleteCategoriesAsync_WithUnexistingCategory_ReturnsNotFound()
        {
            try
            {
                //Arrange
                var existingCategory = CreateRandomCategory();

                categoryRepositoryStub.Setup(repo => repo.GetCategoryAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Category)null);

                var controller = new CategoryController(categoryRepositoryStub.Object);

                //Act
                var result = await controller.DeleteCategoryAsync(existingCategory.Id);

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
