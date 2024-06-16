using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Repository.ResponseHandler;
using Animalsy.BE.Services.VisitAPI.Services;
using System.Collections.Concurrent;

namespace Animalsy.BE.Services.VisitAPI.Repository.Builder;

public class VisitResponseBuilder : IVisitResponseBuilder
{
    private readonly List<Task> _tasks = new();
    private readonly ConcurrentDictionary<string, string> _responseDetails = new();
    private ContractorDto _contractor;
    private CustomerDto _customer;
    private PetDto _pet;
    private ProductDto _product;
    private VendorDto _vendor;
    private readonly IApiService _apiService;
    private readonly IResponseHandler _responseHandler;
    private readonly VisitDto _visit;

    public VisitResponseBuilder(IApiService apiService, IResponseHandler responseHandler, VisitDto visit)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        _visit = visit ?? throw new ArgumentNullException(nameof(visit));
    }

    public IVisitResponseBuilder WithCustomer()
    {
        _tasks.Add(Task.Run(async () =>
        {
            _customer = await _responseHandler.EvaluateResponse<CustomerDto>(nameof(_customer), _responseDetails,
                async () =>
                    await _apiService.GetAsync("CustomerApiClient", $"Api/Customer/{_visit.CustomerId}")
                        .ConfigureAwait(false));
        }));
        return this;
    }

    public IVisitResponseBuilder WithContractor()
    {
        _tasks.Add(Task.Run(async () =>
        {
            _contractor = await _responseHandler.EvaluateResponse<ContractorDto>(nameof(_contractor), _responseDetails,
                async () =>
                    await _apiService.GetAsync("ContractorApiClient", $"Api/Contractor/{_visit.ContractorId}")
                        .ConfigureAwait(false));
        }));

        return this;
    }

    public IVisitResponseBuilder WithPet()
    {
        _tasks.Add(Task.Run(async () =>
        {
            _pet = await _responseHandler.EvaluateResponse<PetDto>(nameof(_pet), _responseDetails,
                () => _apiService.GetAsync("PetApiClient", $"Api/Pet/{_visit.PetId}"));
        }));

        return this;
    }

    public IVisitResponseBuilder WithProduct()
    {
        _tasks.Add(Task.Run(async () =>
        {
            _product = await _responseHandler.EvaluateResponse<ProductDto>(nameof(_product), _responseDetails,
                () => _apiService.GetAsync("ProductApiClient", $"Api/Product/{_visit.ProductId}"));
        }));

        return this;
    }

    public IVisitResponseBuilder WithVendor()
    {
        _tasks.Add(Task.Run(async () =>
        {
            _vendor = await _responseHandler.EvaluateResponse<VendorDto>(nameof(_vendor), _responseDetails,
                () => _apiService.GetAsync("VendorApiClient", $"Api/Vendor/{_visit.VendorId}"));
        }));

        return this;
    }

    public async Task<VisitResponseDto> BuildAsync()
    {
        await Task.WhenAll(_tasks).ConfigureAwait(false);

        return new VisitResponseDto
        {
            Id = _visit.Id,
            Comment = _visit.Comment,
            Date = _visit.Date,
            Status = _visit.Status,
            Contractor = _contractor,
            Customer = _customer,
            Pet = _pet,
            Product = _product,
            Vendor = _vendor,
            ResponseDetails = _responseDetails
        };
    }
}