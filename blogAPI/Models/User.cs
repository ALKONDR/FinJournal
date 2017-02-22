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
        public List<Story> Stories { get; set; } = null;
        public string AccountDescription { get; set; } = string.Empty;
        public List<User> Followers { get; set; } = null;
        public List<User> Following { get; set; } = null;
        public List<Story> Recommendations { get; set; } = null;
        public List<Comment> Responses { get; set; } = null;
        public List<Story> Bookmarks { get; set; } = null;
        public List<Tag> Tags { get; set; } = null;

        public User(string login, string email, string AccountDescription)
        {
            this.AccountDescription = AccountDescription;
            this.Login = login;
            this.Email = email;
        }
    }
}