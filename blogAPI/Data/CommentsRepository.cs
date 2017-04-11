using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class CommentsRepository
    {
        private readonly ILogger _logger;
        private readonly StoriesRepository _storiesRepository;
        public CommentsRepository(StoriesRepository storiesRepository, ILogger<CommentsRepository> logger)
        {
            _storiesRepository = storiesRepository;
            _logger = logger;
        }
        public async Task<ICollection<Comment>> GetAllCommentsAsync(string userName, string title)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                return story.Comments;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all comments\n {e.Message}");
            }

            return null;
        }
        public async Task<bool> AddCommentAsync(string userName, string title, Comment comment)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                if (story == null)
                    return false;
                
                comment.Author = userName;
                comment.Id = story.Comments.Count;
                comment.Date = DateTime.Now;

                story.Comments.Add(comment);

                return await _storiesRepository.UpdateStoryAsync(userName, title, story);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding comment\n {e.Message}");
            }

            return false;
        }
    }
}