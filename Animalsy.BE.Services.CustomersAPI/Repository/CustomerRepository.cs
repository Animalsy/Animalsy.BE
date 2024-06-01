using Animalsy.BE.Services.CustomerAPI.Data;
using Animalsy.BE.Services.CustomerAPI.Models;
using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository.Builder;
using Animalsy.BE.Services.CustomerAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomerAPI.Repository;

public class CustomerRepository(AppDbContext dbContext, IApiService apiService, IMapper mapper) : ICustomerRepository
{
    public async Task<Guid> CreateAsync(CreateCustomerDto customerDto)
    {
        var customer = mapper.Map<Customer>(customerDto);
        await dbContext.Customers.AddAsync(customer);
        await dbContext.SaveChangesAsync();
        return customer.Id;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var results = await dbContext.Customers.ToListAsync();
        return mapper.Map<IEnumerable<CustomerDto>>(results);
    }

    public async Task<CustomerDto> GetByIdAsync(Guid customerId)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        return mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetByEmailAsync(string email)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.EmailAddress == email);
        return mapper.Map<CustomerDto>(customer);
    }
    public async Task<CustomerProfileDto> GetCustomerProfileAsync(Guid customerId)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        return customer != null
            ? await new CustomerProfileBuilder(apiService, mapper.Map<CustomerDto>(customer))
                .WithPets()
                .WithVisits()
                .BuildAsync()
            : null;
    }

    public async Task<bool> TryUpdateAsync(UpdateCustomerDto customerDto)
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