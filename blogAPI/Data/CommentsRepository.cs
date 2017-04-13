using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        /// <summary>
        /// returns all the comments of the story with the given author and title
        /// </summary>
        /// <param name="userName">author's userName</param>
        /// <param name="title">title of the story</param>
        /// <returns>all the story's comments</returns>
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
        /// <summary>
        /// returns a comment with the given id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Comment> GetCommentByIdAsync(string userName, string title, int id)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                int index = story.Comments.FindIndex(com => com.Id == id);

                if (index != -1)
                    return story.Comments[index];
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting comment by id\n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="title"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public async Task<bool> AddCommentAsync(string userName, string title, Comment comment)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                if (story == null)
                    return false;
                
                if (story.Comments.Count == 0)
                    comment.Id = story.Comments.Count;
                else
                    comment.Id = story.Comments[story.Comments.Count - 1].Id + 1; // last id + 1

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
        /// <summary>
        /// deletes comment with the given id
        /// </summary>
        /// <param name="userName">author of the story</param>
        /// <param name="title">title of the story</param>
        /// <param name="id">id of the comment</param>
        /// <returns>if the comment was deleted</returns>
        public async Task<bool> DeleteCommentAsync(string userName, string title, int id)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                int index = story.Comments.FindIndex(com => com.Id == id);

                if (index != -1)
                {
                    story.Comments.RemoveAt(index);
                    await _storiesRepository.UpdateStoryAsync(userName, title, story);
                    
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting comment\n {e.Message}");
            }

            return false;
        }
        /// <summary>
        /// updates given comment
        /// </summary>
        /// <param name="userName">author of the story</param>
        /// <param name="title">title of the story</param>
        /// <param name="comment">new changed comment</param>
        /// <returns>if the comment was updated</returns>
        public async Task<bool> UpdateCommentAsync(string userName, string title, Comment comment)
        {
            try
            {
                Story story = await _storiesRepository.GetStoryByTitleAsync(userName, title);

                int index = story.Comments.FindIndex(com => com.Id == comment.Id);

                if (index != -1)
                {
                    story.Comments[index] = comment;
                    
                    return await _storiesRepository.UpdateStoryAsync(userName, title, story);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating comment\n {e.Message}");
            }

            return false;
        }
    }
}