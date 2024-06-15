using System.Collections.Concurrent;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VendorAPI.Services;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public class VendorProfileBuilder : IVendorProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private readonly ConcurrentDictionary<string, string> _responseDetails = new();
    private IEnumerable<ContractorDto> _contractors;
    private IEnumerable<VisitDto> _visits;
    private readonly IApiService _apiService;
    private readonly IResponseHandler _responseHandler;
    private readonly VendorDto _vendor;

    public VendorProfileBuilder(IApiService apiService, IResponseHandler responseHandler, VendorDto vendor)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        _vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
    }

    public IVendorProfileBuilder WithContractors()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractors = await _responseHandler.EvaluateResponse<IEnumerable<ContractorDto>>(nameof(_contractors), _responseDetails,
                async () => 
                    await _apiService.GetAsync("ContractorApiClient", $"Api/Contractor/Vendor/{_vendor.Id}")
                        .ConfigureAwait(false));
        }));

        return this;
    }

    public IVendorProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await _responseHandler.EvaluateResponse<IEnumerable<VisitDto>>(nameof(_visits), _responseDetails,
                async () =>
                    await _apiService.GetAsync("VisitApiClient", $"Api/Visit/Vendor/{_vendor.Id}")
                        .ConfigureAwait(false));
        }));
        return this;
    }

    public async Task<VendorProfileDto> BuildAsync()
    {

        while (_builderQueue.TryDequeue(out var currentTask))
        {
            await currentTask.ConfigureAwait(false);
        }

        return new VendorProfileDto
        {
            Vendor = _vendor,
            Contractors = _contractors,
            Visits = _visits,
            ResponseDetails = _responseDetails
        };
    }
}
