using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using blogAPI.Models;

namespace blogAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> AllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task AddUser(User user);
        Task<ReplaceOneResult> UpdateUser(User user);
        Task<DeleteResult> RemoveUser(string id);
    }
}