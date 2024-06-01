using Animalsy.BE.Services.CustomerAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder;

public interface ICustomerProfileBuilder
{
    ICustomerProfileBuilder WithPets();
    ICustomerProfileBuilder WithVisits();
    Task<CustomerProfileDto> BuildAsync();
}