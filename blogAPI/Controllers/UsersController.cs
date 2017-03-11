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
        private readonly UsersRepository _usersRepository;
        private readonly ILogger _logger;
        public UsersController(ILogger<UsersController> logger, UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _usersRepository.GetAllUsersAsync();
                return Ok(_usersRepository.GetAllUsersAsync());
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
                if (await _usersRepository.AddUserAsync(user))
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
                var user = await _usersRepository.GetUserByUserNameAsync(userName);
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
                if (await _usersRepository.RemoveUserByUserNameAsync(userName))
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
                if (await _usersRepository.UpdateUserAsync(userName, user))
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