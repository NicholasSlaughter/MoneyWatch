using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Dtos;
using MoneyWatch.API.Models;
using MoneyWatch.API.Repositories;
using MoneyWatch.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUsersRepository _applicationUsersRepository;

        public ApplicationUserController(IApplicationUsersRepository repository) => _applicationUsersRepository = repository;

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDto>> GetApplicationUserAsync(string id)
        {
            var user = await _applicationUsersRepository.GetApplicationUserAsync(id);

            if(user is null)
                return NotFound();

            return Ok(user.AsDto());
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationUserDto>> GetApplicationUsersAsync()
        {
            var users = _applicationUsersRepository.GetApplicationUsersAsync();
            return (await users).Select(user => user.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUserDto>> CreateApplicationUserAsync(CreateApplicationUserDto createApplicationUserDto, string userPassword)
        {
            if(!ModelState.IsValid)
            {
                //TODO: Error handler
                return NotFound();
            }

            var newUser = new ApplicationUser
            {
                UserName = createApplicationUserDto.UserName,
                Email = createApplicationUserDto.Email,
                First_Name = createApplicationUserDto.First_Name,
                Last_Name = createApplicationUserDto.Last_Name,
                Expenses = null,
                Alerts = null
            };
            await _applicationUsersRepository.CreateApplicationUserAsync(newUser, userPassword);
            return CreatedAtAction(nameof(CreateApplicationUserAsync), new { id = newUser.Id }, newUser.AsDto());
        }
    }
}
