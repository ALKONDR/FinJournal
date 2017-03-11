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

                await _context.Users.InsertOneAsync(user);
                _logger.LogInformation($"User: {JsonConvert.SerializeObject(user)} was added");
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
            var filter = Builders<User>.Filter.Eq("UserName", userName);

            try
            {
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
                if ((await GetUserByUserNameAsync(user.UserName)) != null)
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
    }
}