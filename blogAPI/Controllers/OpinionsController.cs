using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;

using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories/{title}")]
    public class OpinionsController: BaseController
    {
        private const string COMMENT = "comment";

        private const string STORY = "story";

        private const int DEFAULT_ID = -1;

        private readonly OpinionsRepository _opinionsRepository;
       
        private readonly ILogger _logger;
        
        public OpinionsController(ILogger<OpinionsController> logger,
                                    OpinionsRepository opinionsRepository)
        {
            _opinionsRepository = opinionsRepository;
            _logger = logger;
        }

        [HttpGet("comments/{id}/{opinionType}")]
        [AllowAnonymous]
        public async Task<IActionResult> CommentGet(string userName,
                                                    string title,
                                                    int id,
                                                    string opinionType)
        {
            try
            {
                var result = await _opinionsRepository.GetAllOpinionsAsync(opinionType, COMMENT,
                                                                            userName, title, id);

                if (result != null)
                    return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all comment opinions\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpPost("comments/{id}/{opinionType}/{author}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> CommentPost(string userName,
                                                        string title,
                                                        int id,
                                                        string opinionType,
                                                        string author)
        {
            try
            {
                if (GetClaimByName(SUB) != author)
                    return BadRequest();

                if (await _opinionsRepository.AddOpinionAsync(opinionType, COMMENT, userName, title, id, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding comment opinion\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("comments/{id}/{opinionType}/{author}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> CommentDelete(string userName,
                                                        string title,
                                                        int id,
                                                        string opinionType,
                                                        string author)
        {
            try
            {
                if (GetClaimByName(SUB) != author)
                    return BadRequest();

                if (await _opinionsRepository.DeleteOpinionAsync(opinionType, COMMENT,
                                                                        userName, title, id, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting comment opinion\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpGet("{opinionType}")]
        [AllowAnonymous]
        public async Task<IActionResult> StoryGet(string userName, string title, string opinionType)
        {
            try
            {
                var result = await _opinionsRepository.GetAllOpinionsAsync(opinionType, STORY, userName,
                                                                            title, DEFAULT_ID);
                
                if (result != null)
                    return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all story's opinions\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpPost("{opinionType}/{author}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> StoryPost(string userName, string title, string opinionType, string author)
        {
            try
            {
                if (GetClaimByName(SUB) != author)
                    return BadRequest();

                if (await _opinionsRepository.AddOpinionAsync(opinionType, STORY, userName,
                                                                title, DEFAULT_ID, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding story opinion\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("{opinionType}/{author}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> StoryDelete(string userName, string title, string opinionType, string author)
        {
            try
            {
                if (GetClaimByName(SUB) != author)
                    return BadRequest();

                if (await _opinionsRepository.DeleteOpinionAsync(opinionType, STORY, userName,
                                                                    title, DEFAULT_ID, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting story opinion\n {e.Message}");
            }

            return BadRequest();
        }
    }
}