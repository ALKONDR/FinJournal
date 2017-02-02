using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using blogAPI.Models;

namespace blogAPI.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        [HttpGet()]
        public JsonResult GetAllUserPosts()
        {
            //TODO: MongoDB

            List<Post> allPosts = new List<Post>();
            allPosts.Add(new Post(1, "test text for 1 post"));
            allPosts.Add(new Post(2, "test text for 2 post"));

            return new JsonResult(allPosts);
        }
    }
}