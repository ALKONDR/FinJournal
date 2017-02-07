using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net;
using Microsoft.Extensions.Logging;

using blogAPI.Models;
using blogAPI.Interfaces;


//TODO: remove all bugs and just make it work ;D
namespace blogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            _logger.LogInformation("Returing all users");
            return await _userRepository.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserById(id);
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            try
            {
                return Ok(_userRepository.AddUser(user));
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}