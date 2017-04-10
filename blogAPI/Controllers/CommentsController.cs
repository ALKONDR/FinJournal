using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories/{title}/comments")]
    public class CommentsController: Controller
    {
        private readonly CommentsRepository _commentsRepository;

        private readonly ILogger _logger;

        public CommentsController(ILogger<CommentsController> logger, CommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string userName, string title)
        {
            try
            {
                var result = await _commentsRepository.GetAllCommentsAsync(userName, title);
                
                if (result != null)
                    return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting comments\n {e.Message}");
            }
            
            return BadRequest();
        }
    }
}