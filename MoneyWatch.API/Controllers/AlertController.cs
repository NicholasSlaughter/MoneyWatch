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
    public class AlertController : ControllerBase
    {
        private readonly IAlertsRepository _alertsRepository;
        private readonly Dictionary<string, double> _periodDictionary = new()
        {
            { "Week", 7 },
            { "Month", DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) },
            { "Year", DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365 }
        };

        public AlertController(IAlertsRepository AlertsRepository)
        {
            _alertsRepository = AlertsRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertDto>> GetAlertAsync(Guid id)
        {
            var Alert = await _alertsRepository.GetAlertAsync(id);

            if (Alert is null)
                return NotFound();

            return Ok(Alert.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<AlertDto>> GetAlertsAsync()
        {
            return (await _alertsRepository.GetAlertsAsync()).Select(Alert => Alert.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<AlertDto>> CreateAlertAsync(CreateAlertDto alertToCreate)
        {
            double daysToAdd = _periodDictionary[alertToCreate.Period.Name];

            var newAlert = new Alert
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = alertToCreate.ApplicationUserId,
                Price = alertToCreate.Price,
                AlertCreationDate = DateTimeOffset.UtcNow,
                PeriodStartDate = DateTimeOffset.UtcNow,
                PeriodEndDate = DateTimeOffset.UtcNow.AddDays(daysToAdd),
                PeriodId = alertToCreate.PeriodId,
                Period = alertToCreate.Period,
                CategoryId = alertToCreate.CategoryId,
                Category = alertToCreate.Category
            };

            await _alertsRepository.CreateAlertAsync(newAlert);
            return CreatedAtAction(nameof(CreateAlertAsync), new { id = newAlert.Id }, newAlert.AsDto());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateAlertAsync(Guid AlertId, UpdateAlertDto alertToUpdate)
        {
            var existingAlert = await _alertsRepository.GetAlertAsync(AlertId);

            if (existingAlert is null)
                return NotFound();

            double daysToAdd = _periodDictionary[existingAlert.Period.Name];

            Alert updatedAlert = existingAlert;

            updatedAlert.Price = alertToUpdate.Price;
            updatedAlert.PeriodStartDate = DateTimeOffset.UtcNow;
            updatedAlert.PeriodEndDate = DateTimeOffset.UtcNow.AddDays(daysToAdd);

            await _alertsRepository.UpdateAlertAsync(updatedAlert);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlertAsync(Guid id)
        {
            var existingAlert = await _alertsRepository.GetAlertAsync(id);

            if (existingAlert is null)
                return NotFound();

            await _alertsRepository.DeleteAlertAsync(existingAlert.Id);

            return NoContent();
        }
    }
}
