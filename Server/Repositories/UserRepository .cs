using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "users";

        public UserRepository(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _filePath = configuration["Data:UserFilePath"] ?? _cacheKey;
        }

        public async Task<List<User>> GetAllAsync()
        {
            if (!_cache.TryGetValue(_cacheKey, out List<User> customers))
            {
                var json = await File.ReadAllTextAsync(_filePath);
                customers = JsonSerializer.Deserialize<List<User>>(json) ?? new();
                _cache.Set(_cacheKey, customers);
            }
            return customers;
        }

        public async Task<User?> GetByusernameAsync(string username)
        {
            return (await GetAllAsync()).FirstOrDefault(c => c.Username == username);
        }
    }
}
