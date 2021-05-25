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
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodsRepository _periodsRepository;

        public PeriodController(IPeriodsRepository PeriodsRepository)
        {
            _periodsRepository = PeriodsRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodDto>> GetPeriodAsync(Guid id)
        {
            var Period = await _periodsRepository.GetPeriodAsync(id);

            if (Period is null)
                return NotFound();

            return Ok(Period.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<PeriodDto>> GetPeriodsAsync()
        {
            return (await _periodsRepository.GetPeriodsAsync()).Select(Period => Period.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<PeriodDto>> CreatePeriodAsync(CreatePeriodDto periodToCreate)
        {
            var newPeriod = new Period
            {
                Id = Guid.NewGuid(),
                Name = periodToCreate.Name
            };

            await _periodsRepository.CreatePeriodAsync(newPeriod);
            return CreatedAtAction(nameof(CreatePeriodAsync), new { id = newPeriod.Id }, newPeriod.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdatePeriodAsync(Guid PeriodId, UpdatePeriodDto periodToUpdate)
        {
            var existingPeriod = await _periodsRepository.GetPeriodAsync(PeriodId);

            if (existingPeriod is null)
                return NotFound();

            Period updatedPeriod = existingPeriod;

            updatedPeriod.Name = periodToUpdate.Name;

            await _periodsRepository.UpdatePeriodAsync(updatedPeriod);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePeriodAsync(Guid id)
        {
            var existingPeriod = await _periodsRepository.GetPeriodAsync(id);

            if (existingPeriod is null)
                return NotFound();

            await _periodsRepository.DeletePeriodAsync(existingPeriod.Id);

            return NoContent();
        }
    }
}
