using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Data;
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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpensesRepository _expensesRepository;

        public ExpenseController(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpenseAsync(Guid id)
        {
            var expense = await _expensesRepository.GetExpenseAsync(id);

            if (expense is null)
                return NotFound();

            return Ok(expense.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<ExpenseDto>> GetExpensesAsync()
        {
            return (await _expensesRepository.GetExpensesAsync()).Select(expense => expense.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ExpenseDto>> CreateExpenseAsync(CreateExpenseDto expenseToCreate)
        {
            var newExpense = new Expense
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = expenseToCreate.ApplicationUserId,
                Price = expenseToCreate.Price,
                Description = expenseToCreate.Description,
                DateOfExpense = DateTimeOffset.UtcNow,
                CategoryId = expenseToCreate.CategoryId
            };

            await _expensesRepository.CreateExpenseAsync(newExpense);
            return CreatedAtAction(nameof(CreateExpenseAsync), new { id = newExpense.Id }, newExpense.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateExpenseAsync(Guid expenseId, UpdateExpenseDto expenseToUpdate)
        {
            var existingExpense = await _expensesRepository.GetExpenseAsync(expenseId);

            if (existingExpense is null)
                return NotFound();

            Expense updatedExpense = existingExpense;

            updatedExpense.Price = expenseToUpdate.Price;
            updatedExpense.Description = expenseToUpdate.Description;
            updatedExpense.CategoryId = expenseToUpdate.CategoryId;

            await _expensesRepository.UpdateExpenseAsync(updatedExpense);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpenseAsync(Guid id)
        {
            var existingExpense = await _expensesRepository.GetExpenseAsync(id);

            if (existingExpense is null)
                return NotFound();

            await _expensesRepository.DeleteExpenseAsync(existingExpense.Id);

            return NoContent();
        }
    }
}
