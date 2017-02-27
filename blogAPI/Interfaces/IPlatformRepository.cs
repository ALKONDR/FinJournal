using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using blogAPI.Models;

namespace blogAPI.Interfaces
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<bool> AddUserAsync(User user);
        Task<ReplaceOneResult> UpdateUserAsync(User user);
        Task<DeleteResult> RemoveUserByIdAsync(string id);
    }
}