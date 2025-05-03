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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _filePath;
        private readonly IMemoryCache _cache; // Thread-safe
        private readonly static string _cacheKey = "customers";

        public CustomerRepository(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _filePath = configuration["Data:CustomerFilePath"] ?? _cacheKey;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            if (!_cache.TryGetValue(_cacheKey, out List<Customer> customers))
            {
                lock (_cacheKey)
                {
                    var json = File.ReadAllText(_filePath); // Sync read
                    customers = JsonSerializer.Deserialize<List<Customer>>(json) ?? new();
                    _cache.Set(_cacheKey, customers);
                }
            }
            return customers;

        }

        public async Task<Customer?> GetByIdAsync(int id) => (await GetAllAsync()).FirstOrDefault(c => c.Id == id);

        public async Task InsertAsync(Customer customer)
        {
            var customers = await GetAllAsync();
            customer.Id = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;
            customers.Add(customer);
            await SaveAsync(customers);
        }

        public async Task UpdateAsync(int id, Customer updated)
        {
            var customers = await GetAllAsync();
            var index = customers.FindIndex(c => c.Id == id);
            if (index >= 0)
            {
                updated.Id = id;
                customers[index] = updated;
                await SaveAsync(customers);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var customers = await GetAllAsync();
            customers.RemoveAll(c => c.Id == id);
            await SaveAsync(customers);
        }

        private async Task SaveAsync(List<Customer> customers)
        {
            lock (_cacheKey)
            {
                var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
                _cache.Set(_cacheKey, customers); // Refresh cache
            }
        }
    }
}
