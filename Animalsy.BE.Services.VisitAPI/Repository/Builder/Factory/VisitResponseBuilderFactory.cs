using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VisitAPI.Services;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder.Factory;

public class VisitResponseBuilderFactory : IVisitResponseBuilderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public VisitResponseBuilderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IVisitResponseBuilder Create(VisitDto visit)
    {
        var apiService = _serviceProvider.GetRequiredService<IApiService>();
        var responseHandler = _serviceProvider.GetRequiredService<IResponseHandler>();
        return new VisitResponseBuilder(apiService, responseHandler, visit);
    }
}