using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoDatabase _settings;
        public UserRepository()
        {
            _database = Connect();
        }
        void IUserRepository.Add(User user)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.AllUsers()
        {
            throw new NotImplementedException();
        }

        User IUserRepository.GetUserById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.Remove(ObjectId id)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.Update(User user)
        {
            throw new NotImplementedException();
        }

        private IMongoDatabase Connect()
        {
            //TODO: create MongoDB connection
        }
    }
}