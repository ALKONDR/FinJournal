using System.Collections.Generic;
using MongoDB.Bson;

namespace blogAPI.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public List<Post> Posts;
        public User(string login, string email)
        {
            this.Login = login;
            this.Email = email;
        }
    }
}