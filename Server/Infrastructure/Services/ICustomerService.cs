using Infrastructure.Models;

namespace Infrastructure.Services
{
    public interface ICustomerService
    {
        Task DeleteAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task InsertAsync(Customer dto);
        Task UpdateAsync(int id, Customer dto);
    }
}