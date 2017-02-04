using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

using blogAPI.Models;

namespace blogAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task AddUser(User user);
        Task<ReplaceOneResult> UpdateUser(User user);
        Task<DeleteResult> RemoveUserById(string id);
    }
}