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
        /// method to get collection for users
        /// </summary>
        /// <returns>MongoDB Collection for Users</returns>
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
    }
}