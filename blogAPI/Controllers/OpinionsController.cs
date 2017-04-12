using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories/{title}")]
    public class OpinionsController: Controller
    {
        private readonly OpinionsRepository _opinionsRepository;
        private readonly ILogger _logger;
        public OpinionsController(ILogger<OpinionsController> logger, OpinionsRepository opinionsRepository)
        {
            _opinionsRepository = opinionsRepository;
            _logger = logger;
        }

        [HttpGet("comments/{Id}/{type}")]
        public async Task<IActionResult> CommentGet(string userName, string title, int Id, string type)
        {
            try
            {
                var result = await _opinionsRepository.GetAllCommentOpinionsAsync(type, userName, title, Id);

                if (result != null)
                    return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all comment opinions\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpPost("comments/{Id}/{type}/{author}")]
        public async Task<IActionResult> CommentPost(string userName, string title, int Id, string type, string author)
        {
            try
            {
                if (await _opinionsRepository.AddCommentOpinion(type, userName, title, Id, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding comment opinion\n {e.Message}");
            }

            return BadRequest();
        }

        [HttpDelete("comments/{Id}/{type}/{author}")]
        public async Task<IActionResult> CommentDelete(string userName, string title, int Id, string type, string author)
        {
            try
            {
                if (await _opinionsRepository.DeleteCommentOpinionAsync(type, userName, title, Id, author))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting comment opinion\n {e.Message}");
            }

            return BadRequest();
        }
    }
}