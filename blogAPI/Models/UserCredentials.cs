using System.ComponentModel.DataAnnotations;

namespace blogAPI.Models
{
    public class UserCredentials
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
    }
}