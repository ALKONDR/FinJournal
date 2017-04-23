using System;
using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/tags")]
    public class TagsController : BaseController
    {
        private readonly TagsRepository _tagsRepository;
        
        private readonly ILogger _logger;
        
        public TagsController(ILogger<TagsController> logger, TagsRepository tagsRepository)
        {
            _tagsRepository = tagsRepository;
            _logger = logger;
        }

        [HttpGet("{tag}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string tag)
        {
            try
            {
                return Ok(await _tagsRepository.GetTagAsync(tag));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting tag\n {e.Message}");
            }

            return BadRequest();
        }
    }
}