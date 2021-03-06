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
    public class UsersRepository
    {
        /// <summary>
        /// Our MongoDB Context to work
        /// </summary>
        private readonly DBContext _context;
        private readonly ILogger _logger;
        public UsersRepository(IOptions<Settings> settings, ILogger<UsersRepository> logger)
        {
            _context = new DBContext(settings);
            _logger = logger;
        }
        
        /// <summary>
        /// adds user to MongoDB
        /// </summary>
        /// <param name="user">User to add</param>
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                if ((await GetUserByUserNameAsync(user.UserName)) != null)
                    return false;
                
                user.Id = new ObjectId();

                await _context.Users.InsertOneAsync(user);
                
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding user \n {e.Message}");
            }
            return false;
        }
        /// <summary>
        /// return all Users from MongoDB
        /// </summary>
        /// <returns>List of all Users or null</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting all users \n {e.Message}");
            }

            if (_context.Users == null)
                _logger.LogInformation("No users in repository");
            
            return null;
        }
        /// <summary>
        /// returns user by given UserName
        /// </summary>
        /// <param name="userName">UserName to get</param>
        /// <returns>user or null</returns>
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("UserName", userName);
                
                return await _context.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting user by UserName \n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// removes user by given UserName
        /// </summary>
        /// <param name="userName">UserName to remove the user</param>
        /// <returns>DeleteResult</returns>
        public async Task<bool> RemoveUserByUserNameAsync(string userName)
        {
            try
            {
                var result = await _context.Users.DeleteOneAsync(u => u.UserName == userName);

                if (result.DeletedCount > 0)
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Removing user by UserName \n {e.Message}");
            }

            return false;
        }
        /// <summary>
        /// update user by UserName
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="user">user to update</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAsync(string userName, User user)
        {
            try
            {
                if (userName != user.UserName && (await GetUserByUserNameAsync(user.UserName)) != null)
                    return false;

                var result = await _context.Users.ReplaceOneAsync(u => u.UserName.Equals(userName), user);
                if (result.ModifiedCount > 0)
                    return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating user \n {e.Message}");
            }
            
            return false;
        }
        /// <summary>
        /// adds follower
        /// </summary>
        /// <param name="following">userName to follow</param>
        /// <param name="follower">follower</param>
        /// <returns>if follower was added</returns>
        public async Task<bool> AddFollowerAsync(string follower, string following)
        {
            try
            {
                var userFollowing = await GetUserByUserNameAsync(following);
                var userFollower = await GetUserByUserNameAsync(follower);

                if (userFollowing == null || userFollower == null)
                    return false;

                userFollowing.Followers.Add(follower);
                userFollower.Following.Add(following);

                bool updateFollowing = await UpdateUserAsync(following, userFollowing);
                bool updateFollower = await UpdateUserAsync(follower, userFollower);

                return updateFollowing && updateFollower;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while following\n {e.Message}");
            }

            return false;
        }
        /// <summary>
        /// removes follower
        /// </summary>
        /// <param name="follower">follower to remove</param>
        /// <param name="following">following user</param>
        /// <returns>if the follower was removed</returns>
        public async Task<bool> RemoveFollowerAsync(string follower, string following)
        {
            try
            {
                var userFollowing = await GetUserByUserNameAsync(following);
                var userFollower = await GetUserByUserNameAsync(follower);

                if (userFollowing == null || userFollower == null)
                    return false;

                userFollowing.Followers.Remove(follower);
                userFollower.Following.Remove(following);

                bool updateFollowing = await UpdateUserAsync(following, userFollowing);
                bool updateFollower = await UpdateUserAsync(follower, userFollower);

                return updateFollowing && updateFollower;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while removing follower\n {e.Message}");
            }

            return false;
        }

        public async Task<IEnumerable<Story>> GetUserNewsAsync(string userName)
        {
            try
            {
                List<Story> news = new List<Story>();

                User user = await GetUserByUserNameAsync(userName);

                if (user == null)
                    return null;

                foreach(string following in user.Following)
                {
                    foreach(Story story in (await GetUserByUserNameAsync(following)).Stories)
                    {
                        news.Add(story);
                    }
                }

                return news;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting user's news\n {e.Message}");
            }

            return null;
        }
    }
}