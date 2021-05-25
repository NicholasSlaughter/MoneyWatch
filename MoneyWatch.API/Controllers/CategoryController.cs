using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Dtos;
using MoneyWatch.API.Models;
using MoneyWatch.API.Repositories;
using MoneyWatch.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoryController(ICategoriesRepository CategoriesRepository)
        {
            _categoriesRepository = CategoriesRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(Guid id)
        {
            var Category = await _categoriesRepository.GetCategoryAsync(id);

            if (Category is null)
                return NotFound();

            return Ok(Category.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return (await _categoriesRepository.GetCategoriesAsync()).Select(Category => Category.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryToCreate)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryToCreate.Name
            };

            await _categoriesRepository.CreateCategoryAsync(newCategory);
            return CreatedAtAction(nameof(CreateCategoryAsync), new { id = newCategory.Id }, newCategory.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateCategoryAsync(Guid CategoryId, UpdateCategoryDto categoryToUpdate)
        {
            var existingCategory = await _categoriesRepository.GetCategoryAsync(CategoryId);

            if (existingCategory is null)
                return NotFound();

            Category updatedCategory = existingCategory;

            updatedCategory.Name = categoryToUpdate.Name;

            await _categoriesRepository.UpdateCategoryAsync(updatedCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await _categoriesRepository.GetCategoryAsync(id);

            if (existingCategory is null)
                return NotFound();

            await _categoriesRepository.DeleteCategoryAsync(existingCategory.Id);

            return NoContent();
        }
    }
}
