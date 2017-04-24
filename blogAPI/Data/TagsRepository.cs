using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
// using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    /// <summary>
    /// class for easier work with MongoDB
    /// </summary>
    public class TagsRepository
    {
        /// <summary>
        /// Our MongoDB Context to work
        /// </summary>
        private readonly DBContext _context;
        private readonly ILogger _logger;
        public TagsRepository(IOptions<Settings> settings,
                                ILogger<TagsRepository> logger)
        {
            _context = new DBContext(settings);
            _logger = logger;
        }
        /// <summary>
        /// returns tag from MongoDB
        /// </summary>
        /// <param name="tag">tag context</param>
        /// <returns>tag if such exists</returns>
        public async Task<Models.Tag> GetTagAsync(string tag)
        {
            try
            {
                var filter = Builders<Models.Tag>.Filter.Eq("Context", tag);

                return await _context.Tags.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting tag\n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// adds story in given tag
        /// </summary>
        /// <param name="userName">story's author</param>
        /// <param name="story">story's title</param>
        /// <param name="tag">tag to add</param>
        /// <returns>if the story was added</returns>
        public async Task<bool> AddStoryInTagAsync(string userName, string story, string tag)
        {
            try
            {
                var tagFromDB = await GetTagAsync(tag);

                if (tagFromDB == null)
                {
                    var tagToAdd = new Models.Tag(tag);
                    tagToAdd.Stories.Add(new Tuple<string, string>(userName, story));

                    await _context.Tags.InsertOneAsync(tagToAdd);

                    return true;
                }

                tagFromDB.Stories.Add(new Tuple<string, string>(userName, story));

                var result = await _context.Tags.ReplaceOneAsync(t => t.Context.Equals(tag), tagFromDB);

                if (result.ModifiedCount > 0)
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding story in tag\n {e.Message}");
            }

            return false;
        }
    }
}