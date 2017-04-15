using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace blogAPI.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Story> Stories { get; set; } = new List<Story>();
        public string AccountDescription { get; set; } = string.Empty;
        public List<string> Followers { get; set; } = new List<string>();
        public List<string> Following { get; set; } = new List<string>();
        public List<string> Recommendations { get; set; } = new List<string>();
        public List<string> Responses { get; set; } = new List<string>();
        public List<string> Bookmarks { get; set; } = new List<string>();
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public User(UserCredentials credentials)
        {
            this.Id = new ObjectId();
            this.UserName = credentials.UserName;
            this.Email = credentials.Email;
        }
    }
}