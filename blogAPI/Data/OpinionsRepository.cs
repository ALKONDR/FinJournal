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
        private const string LIKE = "like";
        private const string DISLIKE = "dislike";
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
        public async Task<bool> AddCommentOpinion(string type, string userName, string title, int Id, Opinion opinion)
        {
            try
            {
                var comment = await _commentsRepository.GetCommentByIdAsync(userName, title, Id);
                
                switch (type)
                {
                    case LIKE:
                        comment.Likes.Add(opinion);
                        break;
                    case DISLIKE:
                        comment.Dislikes.Add(opinion);
                        break;
                    default:
                        _logger.LogDebug($"invalid type of the opinion");
                        break;
                }

                return await _commentsRepository.UpdateCommentAsync(userName, title, comment);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding comment opinion\n {e.Message}");
            }

            return false;
        }
    }
}