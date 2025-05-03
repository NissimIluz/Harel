using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task DeleteAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task InsertAsync(Customer customer);
        Task UpdateAsync(int id, Customer updated);
    }
}