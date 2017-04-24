using System;
using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

using blogAPI.Data;
using blogAPI.Models;

namespace blogAPI.Controllers
{
    [Route("api/tags")]
    public class TagsController : BaseController
    {
        private readonly TagsRepository _tagsRepository;

        private readonly StoriesRepository _storiesRepository;

        private readonly ILogger _logger;

        public TagsController(ILogger<TagsController> logger,
                                TagsRepository tagsRepository,
                                StoriesRepository storiesRepository)
        {
            _tagsRepository = tagsRepository;
            _storiesRepository = storiesRepository;
            _logger = logger;
        }

        [HttpGet("{tag}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string tag)
        {
            try
            {
                var tagData = await _tagsRepository.GetTagAsync(tag);

                if (tagData == null)
                    return BadRequest();

                var stories = new List<Story>();
                foreach(Tuple<string, string> storyData in tagData.Stories)
                    stories.Add(await _storiesRepository.GetStoryByTitleAsync(storyData.Item1, storyData.Item2));

                return Ok(stories);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting tag\n {e.Message}");
            }

            return BadRequest();
        }
    }
}