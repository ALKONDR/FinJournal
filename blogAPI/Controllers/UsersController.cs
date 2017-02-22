using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using blogAPI.Models;
using blogAPI.Interfaces;
using Microsoft.Extensions.Logging;

namespace blogAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        public UsersController( ILogger<UsersController> logger)
        {
            // _userRepository = userRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] {"value1", "value2222"});
        }
    }
}