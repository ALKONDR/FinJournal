using System.Collections.Generic;
using System.Threading.Tasks;

using blogAPI.Models;

namespace blogAPI.Interfaces
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUserNameAsync(string id);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(string id, User user);
        Task<bool> RemoveUserByUserNameAsync(string id);
    }
}