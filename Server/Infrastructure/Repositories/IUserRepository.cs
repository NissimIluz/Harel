using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByusernameAsync(string username);
    }
}