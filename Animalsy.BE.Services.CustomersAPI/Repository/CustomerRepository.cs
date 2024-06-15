using Animalsy.BE.Services.CustomerAPI.Data;
using Animalsy.BE.Services.CustomerAPI.Models;
using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository.Builder;
using Animalsy.BE.Services.CustomerAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.CustomerAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomerAPI.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IApiService _apiService;
    private readonly IResponseHandler _responseHandler;
    private readonly IMapper _mapper;

    public CustomerRepository(AppDbContext dbContext, IApiService apiService, IResponseHandler responseHandler, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
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

    public async Task<CustomerDto> GetByEmailAsync(string email)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.EmailAddress == email);
        return _mapper.Map<CustomerDto>(customer);
    }
    public async Task<CustomerProfileDto> GetCustomerProfileAsync(Guid customerId)
    {
        var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        return customer != null
            ? await new CustomerProfileBuilder(_apiService, _responseHandler, _mapper.Map<CustomerDto>(customer))
                .WithPets()
                .WithVisits()
                .BuildAsync()
            : null;
    }

    public async Task<bool> TryUpdateAsync(UpdateCustomerDto customerDto)
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerDto.Id);
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

    public async Task<bool> TryDeleteAsync(Guid customerId)
    {
        var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        if (existingCustomer == null) return false;

        _dbContext.Customers.Remove(existingCustomer);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}