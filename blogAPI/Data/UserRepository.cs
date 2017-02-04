using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

using blogAPI.Models;
using blogAPI.Interfaces;

namespace blogAPI.Data
{
    /// <summary>
    /// class for easier work with MongoDB
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Our MongoDB Context to work
        /// </summary>
        private readonly UserContext _context = null;
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }
        /// <summary>
        /// adds user to MongoDB
        /// </summary>
        /// <param name="user">User to add</param>
        public async Task AddUser(User user)
        {
            try
            {
                await _context.Users.InsertOneAsync(user);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Adding user \n {e.Message}");
            }
        }
        /// <summary>
        /// return all Users from MongoDB
        /// </summary>
        /// <returns>List of all Users or null</returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Getting all users \n {e.Message}");
            }

            if (_context == null)
                Console.WriteLine("No users in repository");
            
            return null;
        }
        /// <summary>
        /// returns user by given id
        /// </summary>
        /// <param name="id">user id to get</param>
        /// <returns>user or null</returns>
        public async Task<User> GetUserById(string id)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);

            try
            {
                return await _context.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Getting user by id \n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// removes user by given id
        /// </summary>
        /// <param name="id">user id to remove</param>
        /// <returns>DeleteResult</returns>
        public async Task<DeleteResult> RemoveUserById(string id)
        {
            try
            {
                return await _context.Users.DeleteOneAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Removing user by id \n {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// updates given user
        /// </summary>
        /// <param name="user">user to update</param>
        /// <returns>ReplaceOneResult</returns>
        public async Task<ReplaceOneResult> UpdateUser(User user)
        {
            try
            {
                return await _context.Users.ReplaceOneAsync(u => u.Id.Equals(user.Id), user);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Updating user \n {e.Message}");
            }
            
            return null;
        }
    }
}