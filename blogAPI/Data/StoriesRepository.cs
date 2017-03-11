using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using blogAPI.Models;

namespace blogAPI.Data
{
    public class StoriesRepository
    {
        private readonly DBContext _context;
        private readonly ILogger _logger;
        public StoriesRepository(IOptions<Settings> settings, ILogger<UsersRepository> logger)
        {
            _context = new DBContext(settings);
            _logger = logger;
        }
    }
}