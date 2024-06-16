using Animalsy.BE.Services.CustomerAPI.Data;
using Animalsy.BE.Services.CustomerAPI.Models;
using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository.Builder.Factory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomerAPI.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ICustomerProfileBuilderFactory _customerProfileBuilderFactory;
    private readonly IMapper _mapper;

    public CustomerRepository(AppDbContext dbContext, ICustomerProfileBuilderFactory customerProfileBuilderFactory, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _customerProfileBuilderFactory = customerProfileBuilderFactory ?? throw new ArgumentNullException(nameof(customerProfileBuilderFactory));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Guid> CreateAsync(CreateCustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer.Id;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var results = await _dbContext.Customers.ToListAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(results);
    }

    public async Task<CustomerDto> GetByIdAsync(Guid customerId)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerProfileDto> GetCustomerProfileAsync(Guid userId)
    {
        var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.UserId == userId);
        return customer != null
            ? await _customerProfileBuilderFactory.Create(_mapper.Map<CustomerDto>(customer))
                .WithPets()
                .WithVisits()
                .BuildAsync()
            : null;
    }

    public async Task<bool> TryUpdateAsync(UpdateCustomerDto customerDto)
    {
        var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.UserId == customerDto.UserId);
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

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TryDeleteAsync(Guid userId)
    {
        var existingCustomer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.UserId == userId);
        if (existingCustomer == null) return false;

        _dbContext.Customers.Remove(existingCustomer);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}