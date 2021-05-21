using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyWatch.API.Dtos;
using MoneyWatch.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return null;
        }
    }
}
