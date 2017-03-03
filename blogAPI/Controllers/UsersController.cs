using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MongoDB.Bson;

using blogAPI.Models;
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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _platformRepository.GetAllUsersAsync();
                return Ok(_platformRepository.GetAllUsersAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all users\n {e.Message}");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            try
            {
                user.Id = new ObjectId();
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
        [HttpGet("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                var user = await _platformRepository.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting user by UserName\n {e.Message}");
            }
            return BadRequest();
        }
        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            try
            {
                if (await _platformRepository.RemoveUserByUserNameAsync(userName))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting user by UserName\n {e.Message}");
            }
            return BadRequest();
        }
        //TODO: make work normal
        [HttpPut("{userName}")]
        public async Task<IActionResult> Put(string userName, [FromBody]User user)
        {
            try
            {
                if (await _platformRepository.UpdateUserAsync(userName, user))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating user\n {e.Message}");
            }
            return BadRequest();
        }
    }
}