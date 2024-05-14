using Animalsy.BE.Services.CustomersAPI.Data;
using Animalsy.BE.Services.CustomersAPI.Models;
using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomersAPI.Repository
{
    public class CustomerRepository(AppDbContext dbContext, IMapper mapper) : ICustomerRepository
    {
        public async Task<Guid> CreateAsync(CreateCustomerDto customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            var results = await dbContext.Customers.ToListAsync();
            return mapper.Map<IEnumerable<CustomerResponseDto>>(results);
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(Guid customerId)
        {
            var result = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            return mapper.Map<CustomerResponseDto>(result);
        }

        public async Task<CustomerResponseDto?> GetByEmailAsync(string email)
        {
            var result = await dbContext.Customers.FirstOrDefaultAsync(c => c.EmailAddress == email);
            return mapper.Map<CustomerResponseDto>(result);
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDto customerDto)
        {
            var existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerDto.Id);
            if (existingCustomer == null) return false;

            existingCustomer.Name =  customerDto.Name;
            existingCustomer.LastName = customerDto.LastName;
            existingCustomer.City = customerDto.City;
            existingCustomer.Street = customerDto.Street;
            existingCustomer.Building = customerDto.Building;
            existingCustomer.Flat = customerDto.Flat;
            existingCustomer.PostalCode = customerDto.PostalCode;
            existingCustomer.PhoneNumber = customerDto.PhoneNumber;
            existingCustomer.EmailAddress = customerDto.EmailAddress;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid customerId)
        {
            var existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
            if (existingCustomer == null) return false;

            dbContext.Customers.Remove(existingCustomer);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
