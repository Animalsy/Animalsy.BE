using Animalsy.BE.Services.CustomerAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder;

public interface ICustomerResponseBuilder
{
    ICustomerResponseBuilder WithContractors();
    ICustomerResponseBuilder WithVisits();
    Task<CustomerResponseDto> BuildAsync();
}