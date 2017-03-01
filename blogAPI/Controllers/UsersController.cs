using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using blogAPI.Models;
using blogAPI.Interfaces;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly PlatformRepository _platformRepository;
        private readonly ILogger _logger;
        public UsersController(ILogger<UsersController> logger, PlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_platformRepository.GetAllUsersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            try
            {
                _logger.LogInformation($"Trying to post user \n {JsonConvert.SerializeObject(user)}");
                if (await _platformRepository.AddUserAsync(user))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while posting user {e.Message}");
            }
            return BadRequest();
        }
    }
}