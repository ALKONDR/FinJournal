using System;
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
        public async Task<bool> AddCommentOpinion(string type, string userName, string title, int Id, string author)
        {
            try
            {
                Opinion opinion = new Opinion(author);
                opinion.Date = DateTime.Now;

                var res = await DeleteCommentOpinionAsync(type == LIKE ? DISLIKE : LIKE, userName, title, Id, opinion.Author);
                _logger.LogDebug($"Delete result: {res}");

                var comment = await _commentsRepository.GetCommentByIdAsync(userName, title, Id);
                
                switch (type)
                {
                    case LIKE:                
                        if (comment.Likes.FindIndex(op => op.Author == opinion.Author) == -1)
                            comment.Likes.Add(opinion);
                        break;
                    
                    case DISLIKE:
                        if (comment.Dislikes.FindIndex(op => op.Author == opinion.Author) == -1)
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
        public async Task<bool> DeleteCommentOpinionAsync(string type, string userName, string title, int Id, string author)
        {
            try
            {
                var comment = await _commentsRepository.GetCommentByIdAsync(userName, title, Id);

                int deleteCount = -1;

                switch (type)
                {
                    case LIKE:
                        deleteCount = comment.Likes.RemoveAll(op => op.Author == author);
                        break;
                    
                    case DISLIKE:
                        deleteCount = comment.Dislikes.RemoveAll(op => op.Author == author);
                        break;

                    default:
                        _logger.LogDebug("Invalid type of the opinion");
                        break;
                }

                if (deleteCount > 0)
                    return await _commentsRepository.UpdateCommentAsync(userName, title, comment);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting comment opinion\n {e.Message}");
            }

            return false;
        }
    }
}