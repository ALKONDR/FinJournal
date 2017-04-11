using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class OpinionsRepository
    {
        private readonly ILogger _logger;
        private readonly CommentsRepository _commentsRepository;
        private readonly StoriesRepository _storiesRepository;
        public OpinionsRepository(ILogger<OpinionsRepository> logger, 
                                    CommentsRepository commentsRepository,
                                    StoriesRepository storiesRepository)
        {
            _logger = logger;
            _commentsRepository = commentsRepository;
            _storiesRepository = storiesRepository;
        }
    }
}