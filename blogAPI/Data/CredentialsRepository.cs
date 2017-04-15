using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class CredentialsRepository
    {
        private readonly DBContext _context;
        private readonly UsersRepository _usersRepository;
        private readonly ILogger _logger;
        public CredentialsRepository(IOptions<Settings> settings,
                                    ILogger<CredentialsRepository> logger,
                                    UsersRepository usersRepository)
        {
            _context = new DBContext(settings);
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<bool> AddUserCredentialsAsync(UserCredentials credentials)
        {
            try
            {
                if (credentials.Email.Equals(string.Empty) || credentials.UserName.Equals(string.Empty))
                    return false;

                credentials.Id = new ObjectId();

                if (await _usersRepository.AddUserAsync(new User(credentials)))
                {
                    await _context.Credentials.InsertOneAsync(credentials);
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while registrating user\n {e.Message}");
            }

            return false;
        }

        public async Task<UserCredentials> GetUserCredentialsAsync(string userName)
        {
            try
            {
                var filter = Builders<UserCredentials>.Filter.Eq("UserName", userName);

                return await _context.Credentials.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while getting user credentials\n {e.Message}");
            }

            return null;
        }
    }
}