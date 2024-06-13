using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Services;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public class VendorProfileBuilder : IVendorProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private IEnumerable<ContractorDto> _contractors;
    private IEnumerable<VisitDto> _visits;
    private readonly IApiService _apiService;
    private readonly VendorDto _vendor;

    public VendorProfileBuilder(IApiService apiService, VendorDto vendor)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));
    }

    public IVendorProfileBuilder WithContractors()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractors = await _apiService.GetAsync<IEnumerable<ContractorDto>>("ContractorApiClient", $"Api/Contractor/Vendor/{_vendor.Id}");
        }));

        return this;
    }

    public IVendorProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await _apiService.GetAsync<IEnumerable<VisitDto>>("VisitApiClient", $"Api/Visit/Vendor/{_vendor.Id}");
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
        };
    }
}
