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

        [HttpPost("comment/{Id}/{type}")]
        public async Task<IActionResult> CommentPost(string userName, string title, int Id, string type, [FromBody]Opinion opinion)
        {
            try
            {
                return Ok(await _opinionsRepository.AddCommentOpinion(type, userName, title, Id, opinion));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding comment opinion\n {e.Message}");
            }

            return BadRequest();
        }
    }
}