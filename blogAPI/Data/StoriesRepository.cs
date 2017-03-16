using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class StoriesRepository: UsersRepository
    {
        // private readonly DBContext _context;
        private readonly ILogger _logger;
        public StoriesRepository(IOptions<Settings> settings, ILogger<StoriesRepository> logger) : base(settings, logger)
        {
            _logger = logger;
        }
        public async Task<bool> AddStoryAsync(string userName, Story story)
        {
            try
            {
                story.Id = new ObjectId();
                story.Date = DateTime.Now;
                story.Author = userName;

                User user = await GetUserByUserNameAsync(userName);
                
                if (user == null)
                    return false;

                _logger.LogDebug($"User:\n {JsonConvert.SerializeObject(user)}\n");
                _logger.LogDebug($"New story:\n {JsonConvert.SerializeObject(story)}\n");

                if (user.Stories == null)
                    user.Stories = new List<Story>();
                
                user.Stories.Add(story);
                

                _logger.LogDebug($"New user:\n {JsonConvert.SerializeObject(user)}\n");
                
                if (await UpdateUserAsync(userName, user))
                    return true;

            } catch (Exception e)
            {
                _logger.LogError($"Error while adding story\n {e.Message}");
            }

            return false;
        }
    }
}