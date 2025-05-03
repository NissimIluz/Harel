using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _hashKey;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _hashKey = configuration["AuthSettings:HashKey"] ?? throw new ArgumentNullException("HashKey not found in configuration");
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByusernameAsync(username);
            if (user == null) return false;

            string hashedInput = HashPassword(password);
            return user.HashPassword == hashedInput;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var combined = password + _hashKey;
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
            return Convert.ToBase64String(bytes);
        }
    }

}
