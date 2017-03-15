using System;
using System.Collections.Generic;
using MongoDB.Driver;
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
        public StoriesRepository(IOptions<Settings> settings, ILogger<UsersRepository> logger) : base(settings, logger){}
        public async Task<bool> AddStoryAsync(string userName, Story story)
        {
            try
            {
                var user = await GetUserByUserNameAsync(userName);
                
                if (user == null)
                    return false;

                user.Stories.Add(story);
                
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