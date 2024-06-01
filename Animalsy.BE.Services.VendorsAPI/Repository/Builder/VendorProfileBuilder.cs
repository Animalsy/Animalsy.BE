using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Services;

namespace Animalsy.BE.Services.VendorAPI.Repository.Builder;

public class VendorProfileBuilder(IApiService apiService, VendorDto vendor) : IVendorProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private IEnumerable<ContractorDto> _contractors;
    private IEnumerable<VisitDto> _visits;

    public IVendorProfileBuilder WithContractors()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractors = await apiService.GetAsync<IEnumerable<ContractorDto>>("ContractorApiClient", $"Api/Contractor/Vendor/{vendor.Id}");
        }));

        return this;
    }

    public IVendorProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await apiService.GetAsync<IEnumerable<VisitDto>>("VisitApiClient", $"Api/Visit/Vendor/{vendor.Id}");
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
            OpeningHour = vendor.OpeningHour,
            ClosingHour = vendor.ClosingHour,
            Contractors = _contractors,
            Visits = _visits,
        };
    }
}
