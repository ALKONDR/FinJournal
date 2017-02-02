using System.Collections.Generic;
using MongoDB.Bson;

using blogAPI.Models;

namespace blogAPI.Data
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers();
        User GetUserById(ObjectId id);
        void Add(User user);
        void Update(User user);
        void Remove(ObjectId id);
    }
}