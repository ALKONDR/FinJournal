using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

using blogAPI.Models;
using blogAPI.Data;

namespace blogAPI.Controllers
{
    [Route("api/users/{userName}/stories/{title}")]
    public class CommentsController: Controller
    {
        private readonly StoriesRepository _storiesRepository;

        private readonly ILogger _logger;

        public CommentsController(ILogger<CommentsController> logger, StoriesRepository storiesRepository)
        {
            _storiesRepository = storiesRepository;
            _logger = logger;
        }

        
    }
}