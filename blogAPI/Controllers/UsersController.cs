using System;
using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly UsersRepository _usersRepository;
        
        private readonly ILogger _logger;
        
        public UsersController(ILogger<UsersController> logger, UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {   
                return Ok(await _usersRepository.GetAllUsersAsync());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all users\n {e.Message}");
            }

            return BadRequest();
        }
        
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post([FromBody]User user)
        {
            try
            {
                if ((await _usersRepository.GetUserByUserNameAsync(user.UserName)) != null)
                    return BadRequest("Such userName already exists");

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
        [AllowAnonymous]
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
        [Authorize(Policy = "Admin")]
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
        
        [HttpPut("{userName}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Put(string userName, [FromBody]User user)
        {
            try
            {
                if (GetClaimByName(SUB) != userName)
                    return BadRequest();

                var userFromDB = await _usersRepository.GetUserByUserNameAsync(userName);

                if (userFromDB == null)
                    return BadRequest();

                user.Id = userFromDB.Id;    

                if (await _usersRepository.UpdateUserAsync(userName, user))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating user\n {e.Message}");
            }
            return BadRequest();
        }

        [HttpPost("{follower}/follow/{following}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Follow(string follower, string following)
        {
            try
            {
                if (!GetClaimByName(SUB).Equals(follower))
                    return BadRequest();

                if (await _usersRepository.AddFollowerAsync(follower, following))
                    return Ok();

                _logger.LogDebug("and there");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding a follower\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{follower}/unfollow/{following}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Unfollow(string follower, string following)
        {
            try
            {
                if (GetClaimByName(SUB) != follower)
                    return BadRequest();

                if (await _usersRepository.RemoveFollowerAsync(follower, following))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while removing follower\n {e.Message}");
            }

            return BadRequest();
        }
    }
}