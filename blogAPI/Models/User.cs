using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace blogAPI.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = null;
    }
}