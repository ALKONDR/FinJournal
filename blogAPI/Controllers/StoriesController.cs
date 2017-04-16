using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories")]
    public class StoriesController : BaseController
    {
        private readonly StoriesRepository _storiesRepository;
        
        private readonly ILogger _logger;

        public StoriesController(ILogger<StoriesController> logger, StoriesRepository storiesRepository)
        {
            _storiesRepository = storiesRepository;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Post(string userName, [FromBody]Story story)
        {
            try
            {
                if (GetClaimByName(SUB) != userName)
                    return BadRequest();

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
        [AllowAnonymous]
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
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Delete(string userName, string title)
        {
            try
            {
                if (GetClaimByName(SUB) != userName)
                    return BadRequest();

                if (await _storiesRepository.DeleteStoryAsync(userName, title))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting story\n {e.Message}");
            }
            return BadRequest();
        }
        
        [HttpGet("{title}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string userName, string title)
        {
            try
            {
                var story = await _storiesRepository.GetStoryByTitleAsync(userName, title);
                
                if (story == null)
                    return BadRequest();
                
                return Ok(story);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting story by title\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpPut("{title}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Put(string userName, string title, [FromBody] Story story)
        {
            try
            {
                if (GetClaimByName(SUB) != userName)
                    return BadRequest();
            
                if (await _storiesRepository.UpdateStoryAsync(userName, title, story))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating story\n {e.Message}");
            }
            return BadRequest();
        }
    }
}