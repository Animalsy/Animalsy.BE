using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VendorAPI.Services;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder.Factory;

public class VendorProfileBuilderFactory : IVendorProfileBuilderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public VendorProfileBuilderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IVendorProfileBuilder Create(VendorDto vendor)
    {
        var apiService = _serviceProvider.GetRequiredService<IApiService>();
        var responseHandler = _serviceProvider.GetRequiredService<IResponseHandler>();
        return new VendorProfileBuilder(apiService, responseHandler, vendor);
    }
}