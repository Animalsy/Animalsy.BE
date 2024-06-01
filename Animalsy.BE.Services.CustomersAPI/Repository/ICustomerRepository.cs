using Animalsy.BE.Services.CustomerAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomerAPI.Repository;

public interface ICustomerRepository
{
    Task<Guid> CreateAsync(CreateCustomerDto customerDto);
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid customerId);
    Task<CustomerDto> GetByEmailAsync(string email);
    Task<CustomerProfileDto> GetCustomerProfileAsync(Guid customerId);
    Task<bool> TryUpdateAsync(UpdateCustomerDto customerDto);
    Task<bool> TryDeleteAsync(Guid customerId);
}