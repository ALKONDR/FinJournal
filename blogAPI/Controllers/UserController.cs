using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net;

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
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
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