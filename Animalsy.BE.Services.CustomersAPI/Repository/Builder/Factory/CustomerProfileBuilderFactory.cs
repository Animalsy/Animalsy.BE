using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.CustomerAPI.Services;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder.Factory;

public class CustomerProfileBuilderFactory : ICustomerProfileBuilderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CustomerProfileBuilderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICustomerProfileBuilder Create(CustomerDto customer)
    {
        var apiService = _serviceProvider.GetRequiredService<IApiService>();
        var responseHandler = _serviceProvider.GetRequiredService<IResponseHandler>();
        return new CustomerProfileBuilder(apiService, responseHandler, customer);
    }
}