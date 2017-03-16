using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories")]
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
                _logger.LogError($"Error while adding story\n {e.Message}");
            }

            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                return Ok(await _storiesRepository.GetAllStoriesAsync(userName));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all stories\n {e.Message}");
            }
            return BadRequest();
        }
        [HttpDelete("{title}")]
        public async Task<IActionResult> Delete(string userName, string title)
        {
            try
            {
                if (await _storiesRepository.DeleteStoryAsync(userName, title))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting story\n {e.Message}");
            }
            return BadRequest();
        }
    }
}