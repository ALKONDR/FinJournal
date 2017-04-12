using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;

using blogAPI.Models;
using blogAPI.Interfaces;

namespace blogAPI.Data
{
    public class OpinionsRepository
    {
        private const string STORY = "story";
        private const string COMMENT = "comment";
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

        private async Task<IPostable> GetPostWithGivenType(string postType,
                                                            string userName,
                                                            string title,
                                                            int id)
        {
            try
            {
                switch (postType)
                {
                    case STORY:
                        return await _storiesRepository.GetStoryByTitleAsync(userName, title);
                    
                    case COMMENT:
                        return await _commentsRepository.GetCommentByIdAsync(userName, title, id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting post with given type\n {e.Message}");
            }

            return null;
        }

        private async Task<bool> UpdatePostWithGivenType(string postType,
                                                                string userName,
                                                                string title,
                                                                IPostable post)
        {
            try
            {
                switch (postType)
                {
                    case STORY:
                        return await _storiesRepository.UpdateStoryAsync(userName, title, (Story)post);
                    
                    case COMMENT:
                        return await _commentsRepository.UpdateCommentAsync(userName, title, (Comment)post);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating post with given type\n {e.Message}");
            }

            return false;
        }

        public async Task<ICollection<Opinion>> GetAllOpinionsAsync(string opinionType,
                                                                            string postType,
                                                                            string userName,
                                                                            string title,
                                                                            int id)
        {
            try
            {
                IPostable post = await GetPostWithGivenType(postType, userName, title, id);

                if (post != null)
                {   
                    switch (opinionType)
                    {
                        case LIKE:
                            return post.Likes;

                        case DISLIKE:
                            return post.Dislikes;
                        
                        default:
                            _logger.LogDebug("Invalid type of the opinions");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all opinions\n {e.Message}");
            }

            return null;
        }
        public async Task<bool> AddOpinionAsync(string opinionType,
                                                    string postType,
                                                    string userName,
                                                    string title,
                                                    int Id,
                                                    string author)
        {
            try
            {
                Opinion opinion = new Opinion(author);
                opinion.Date = DateTime.Now;

                var res = await DeleteOpinionAsync(opinionType == LIKE ? DISLIKE : LIKE,
                                                    postType, userName, title, Id, opinion.Author);                

                IPostable post = await GetPostWithGivenType(postType, userName, title, Id);
                
                switch (opinionType)
                {
                    case LIKE:                
                        if (post.Likes.FindIndex(op => op.Author == opinion.Author) == -1)
                            post.Likes.Add(opinion);
                        break;
                    
                    case DISLIKE:
                        if (post.Dislikes.FindIndex(op => op.Author == opinion.Author) == -1)
                            post.Dislikes.Add(opinion);
                        break;
                    
                    default:
                        _logger.LogDebug($"invalid type of the opinion");
                        break;
                }

                return await UpdatePostWithGivenType(postType, userName, title, post);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding opinion\n {e.Message}");
            }

            return false;
        }
        public async Task<bool> DeleteOpinionAsync(string opinionType,
                                                            string postType,
                                                            string userName,
                                                            string title,
                                                            int id,
                                                            string author)
        {
            try
            {
                var post = await GetPostWithGivenType(postType, userName, title, id);

                int deleteCount = -1;

                switch (opinionType)
                {
                    case LIKE:
                        deleteCount = post.Likes.RemoveAll(op => op.Author == author);
                        break;
                    
                    case DISLIKE:
                        deleteCount = post.Dislikes.RemoveAll(op => op.Author == author);
                        break;

                    default:
                        _logger.LogDebug("Invalid type of the opinion");
                        break;
                }

                if (deleteCount > 0)
                    return await UpdatePostWithGivenType(postType, userName, title, post);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting opinion\n {e.Message}");
            }

            return false;
        }
    }
}