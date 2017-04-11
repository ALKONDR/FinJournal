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
    }
}