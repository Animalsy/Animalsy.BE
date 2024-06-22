using Animalsy.BE.Services.CustomerAPI.Models.Dto;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder.Factory;

public interface ICustomerProfileBuilderFactory
{
    ICustomerProfileBuilder Create(CustomerDto customer);
}