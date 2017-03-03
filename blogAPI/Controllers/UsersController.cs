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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _platformRepository.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting user by id\n {e.Message}");
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (await _platformRepository.RemoveUserByIdAsync(id))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting user by id\n {e.Message}");
            }
            return BadRequest();
        }
    }
}