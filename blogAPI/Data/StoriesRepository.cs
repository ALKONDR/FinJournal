using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class StoriesRepository
    {
        private readonly ILogger _logger;
        private readonly UsersRepository _usersRepository;
        public StoriesRepository(UsersRepository usersRepository, ILogger<StoriesRepository> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }
        /// <summary>
        /// allow to get story by given title
        /// </summary>
        /// <param name="userName">userName of the author of the story</param>
        /// <param name="title">story's title</param>
        /// <returns>userName's story with the given title</returns>
        public async Task<Story> GetStoryByTitleAsync(string userName, string title)
        {
            try
            {
                User user = await _usersRepository.GetUserByUserNameAsync(userName);

                Story story = null;

                if (user == null || user.Stories == null || !user.Stories.Exists(s => s.Title.Equals(title)))
                    return null;
                
                story = user.Stories.Find(s => s.Title.Equals(title));

                // _logger.LogDebug($"Story:\n {JsonConvert.SerializeObject(story)}\n");

                return story;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting story by title\n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// adds story to the given user
        /// </summary>
        /// <param name="userName">userName of the author of given story</param>
        /// <param name="story">story to append</param>
        /// <returns>if the story was appended</returns>
        public async Task<bool> AddStoryAsync(string userName, Story story)
        {
            try
            {
                story.Id = new ObjectId();
                story.Date = DateTime.Now;
                story.Author = userName;

                User user = await _usersRepository.GetUserByUserNameAsync(userName);
                
                if (user == null)
                    return false;

                // check if story with such title is already exists
                Story sameStory = await GetStoryByTitleAsync(userName, story.Title);
                if (sameStory != null)
                    return false;

                user.Stories.Add(story);
                
                return await _usersRepository.UpdateUserAsync(userName, user);

            }
            catch (Exception e)
            {
                _logger.LogError($"Error while adding story\n {e.Message}");
            }

            return false;
        }
        /// <summary>
        /// return all stories of the given user
        /// </summary>
        /// <param name="userName">user's userName</param>
        /// <returns>user's stories</returns>
        public async Task<IEnumerable<Story>> GetAllStoriesAsync(string userName)
        {
            try
            {
                User user = await _usersRepository.GetUserByUserNameAsync(userName);

                if (user == null)
                    return null;

                return user.Stories;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting all stories\n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// deletes user's story with the given title
        /// </summary>
        /// <param name="userName">userName of the author of the story</param>
        /// <param name="title">story's title</param>
        /// <returns>if the story was deleted</returns>
        public async Task<bool> DeleteStoryAsync(string userName, string title)
        {
            try
            {
                User user = await _usersRepository.GetUserByUserNameAsync(userName);

                if (user == null || user.Stories == null)
                    return false;

                user.Stories.RemoveAll(story => story.Title == title);

                if (await _usersRepository.UpdateUserAsync(userName, user))
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deleting story\n {e.Message}");
            }
            return false;
        }
        /// <summary>
        /// updates user's story with the given title
        /// </summary>
        /// <param name="userName">user's userName</param>
        /// <param name="title">title of the old story</param>
        /// <param name="story">changed story</param>
        /// <returns>if the story was updated</returns>
        public async Task<bool> UpdateStoryAsync(string userName, string title, Story story)
        {
            try
            {
                User user = await _usersRepository.GetUserByUserNameAsync(userName);

                if (user == null || user.Stories == null || !user.Stories.Exists(s => s.Title == title))
                    return false;

                var index = user.Stories.FindIndex(s => s.Title == title);
                user.Stories[index] = story;

                return await _usersRepository.UpdateUserAsync(userName, user);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while updating story\n {e.Message}");
            }
            return false;
        }
    }
}