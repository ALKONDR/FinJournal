using MongoDB.Driver;
using Microsoft.Extensions.Options;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class DBContext
    {
        private readonly IMongoDatabase _database = null; 
        /// <summary>
        /// creates MongoDB Client
        /// </summary>
        /// <param name="settings">settings for MongoDB Client</param>
        public DBContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }
        /// <summary>
        /// property to get collection of users
        /// </summary>
        /// <returns>MongoDB Collection of Users</returns>
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
        /// <summary>
        /// property to get collection of all tags
        /// </summary>
        /// <returns>MongoDB Collection of all tags</returns>
        public IMongoCollection<Models.Tag> Tags
        {
            get
            {
                return _database.GetCollection<Models.Tag>("Tags");
            }
        }
    }
}