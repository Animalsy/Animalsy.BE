using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Services;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder;

public class VisitResponseBuilder(IApiService apiService, VisitDto visit) : IVisitResponseBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private ContractorDto _contractor;
    private CustomerDto _customer;
    private PetDto _pet;
    private ProductDto _product;
    private VendorDto _vendor;

    public IVisitResponseBuilder WithCustomer()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _customer = await apiService.GetAsync<CustomerDto>("CustomerApiClient", $"Api/Customer/{visit.CustomerId}");
        }));
        return this;
    }

    public IVisitResponseBuilder WithContractor()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractor = await apiService.GetAsync<ContractorDto>("ContractorApiClient", $"Api/Contractor/{visit.ContractorId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithPet()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _pet = await apiService.GetAsync<PetDto>("PetApiClient", $"Api/Pet/{visit.PetId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithProduct()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _product = await apiService.GetAsync<ProductDto>("ProductApiClient", $"Api/Product/{visit.ProductId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithVendor()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _vendor = await apiService.GetAsync<VendorDto>("VendorApiClient", $"Api/Vendor/{visit.VendorId}");
        }));

        return this;
    }

    public async Task<VisitResponseDto> BuildAsync()
    {

        while (_builderQueue.TryDequeue(out var currentTask))
        {
            await currentTask.ConfigureAwait(false);
        }

        return new VisitResponseDto
        {
            Id = visit.Id,
            Comment = visit.Comment,
            Date = visit.Date,
            State = visit.State,
            Contractor = _contractor,
            Customer = _customer,
            Pet = _pet,
            Product = _product,
            Vendor = _vendor,
        };
    }
}