using Animalsy.BE.Services.CustomersAPI.Data;
using Animalsy.BE.Services.CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid customerId)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<bool> TryUpdateAsync(Guid customerId, Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (existingCustomer == null) return false;
            
            existingCustomer.Name = customer.Name;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.City = customer.City;
            existingCustomer.Street = customer.Street;
            existingCustomer.Building = customer.Building;
            existingCustomer.Flat = customer.Flat;
            existingCustomer.PostalCode = customer.PostalCode;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.EmailAddress = customer.EmailAddress;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid customerId)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (existingCustomer == null) return false;

            _dbContext.Customers.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
