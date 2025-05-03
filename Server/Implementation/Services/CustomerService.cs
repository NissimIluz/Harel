using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Customer?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task InsertAsync(Customer customer)
        {
         
            await _repository.InsertAsync(customer);
        }

        public async Task UpdateAsync(int id, Customer updated)
        {
            await _repository.UpdateAsync(id, updated);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }

}
