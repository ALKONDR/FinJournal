using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using blogAPI.Models;

namespace blogAPI.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        [HttpGet()]
        public JsonResult GetAllUsers()
        {
            //TODO: MongoDB

            List<User> allPosts = new List<User>();
            allPosts.Add(new User());
            allPosts.Add(new User());

            return new JsonResult(allPosts);
        }
    }
}