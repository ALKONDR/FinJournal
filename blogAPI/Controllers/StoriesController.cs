using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/posts")]
    public class StoriesController : Controller
    {
        private readonly StoriesRepository _storiesRepository;
        private readonly ILogger _logger;
        public StoriesController(ILogger<StoriesController> logger, StoriesRepository storiesRepository)
        {
            _storiesRepository = storiesRepository;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post(string userName, [FromBody]Story story)
        {
            try
            {
                if (await _storiesRepository.AddStoryAsync(userName, story))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding post\n {e.Message}");
            }

            return BadRequest();
        }
    }
}