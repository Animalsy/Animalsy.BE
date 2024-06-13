using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Services;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder;

public class VisitResponseBuilder : IVisitResponseBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private ContractorDto _contractor;
    private CustomerDto _customer;
    private PetDto _pet;
    private ProductDto _product;
    private VendorDto _vendor;
    private readonly IApiService _apiService;
    private readonly VisitDto _visit;

    public VisitResponseBuilder(IApiService apiService, VisitDto visit)
    {
        _apiService = apiService;
        _visit = visit;
    }

    public IVisitResponseBuilder WithCustomer()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _customer = await _apiService.GetAsync<CustomerDto>("CustomerApiClient", $"Api/Customer/{_visit.CustomerId}");
        }));
        return this;
    }

    public IVisitResponseBuilder WithContractor()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _contractor = await _apiService.GetAsync<ContractorDto>("ContractorApiClient", $"Api/Contractor/{_visit.ContractorId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithPet()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _pet = await _apiService.GetAsync<PetDto>("PetApiClient", $"Api/Pet/{_visit.PetId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithProduct()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _product = await _apiService.GetAsync<ProductDto>("ProductApiClient", $"Api/Product/{_visit.ProductId}");
        }));

        return this;
    }

    public IVisitResponseBuilder WithVendor()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _vendor = await _apiService.GetAsync<VendorDto>("VendorApiClient", $"Api/Vendor/{_visit.VendorId}");
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
            Id = _visit.Id,
            Comment = _visit.Comment,
            Date = _visit.Date,
            State = _visit.State,
            Contractor = _contractor,
            Customer = _customer,
            Pet = _pet,
            Product = _product,
            Vendor = _vendor,
        };
    }
}