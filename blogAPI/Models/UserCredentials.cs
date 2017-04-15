using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace blogAPI.Models
{
    public class UserCredentials
    {
        public ObjectId Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
    }
}