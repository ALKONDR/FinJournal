using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blogAPI.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Story> Stories { get; set; } = null;
        public string AccountDescription { get; set; } = string.Empty;
        public List<string> Followers { get; set; } = null;
        public List<string> Following { get; set; } = null;
        public List<string> Recommendations { get; set; } = null;
        public List<string> Responses { get; set; } = null;
        public List<string> Bookmarks { get; set; } = null;
        public List<Tag> Tags { get; set; } = null;
    }
}