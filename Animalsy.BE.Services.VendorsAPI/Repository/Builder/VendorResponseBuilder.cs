using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Services;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public class VisitResponseBuilder(IApiService apiService, VendorDto vendor) : IVendorResponseBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private IEnumerable<ContractorDto> _contractors;
    private IEnumerable<VisitDto> _visits;

    public IVendorResponseBuilder WithContractors()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractors = await apiService.GetAsync<IEnumerable<ContractorDto>>("ContractorApiClient", $"Api/Contractor/Vendor/{vendor.Id}");
        }));

        return this;
    }

    public IVendorResponseBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await apiService.GetAsync<IEnumerable<VisitDto>>("VisitApiClient", $"Api/Visit/Vendor/{vendor.Id}");
        }));
        return this;
    }


    public async Task<VendorResponseDto> BuildAsync()
    {

        while (_builderQueue.TryDequeue(out var currentTask))
        {
            await currentTask.ConfigureAwait(false);
        }

        return new VendorResponseDto
        {
            Id = vendor.Id,
            Name = vendor.Name,
            Nip = vendor.Nip,
            City = vendor.City,
            Street = vendor.Street,
            Building = vendor.Building,
            Flat = vendor.Flat,
            PostalCode = vendor.PostalCode,
            PhoneNumber = vendor.PhoneNumber,
            EmailAddress = vendor.EmailAddress,
            Contractors = _contractors,
            Visits = _visits,
        };
    }
}
