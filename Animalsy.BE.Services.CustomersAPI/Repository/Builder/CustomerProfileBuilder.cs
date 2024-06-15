using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Services;
using System.Collections.Concurrent;
using Animalsy.BE.Services.CustomerAPI.Repository.ResponseHandler;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder;

public class CustomerProfileBuilder : ICustomerProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private readonly ConcurrentDictionary<string, string> _responseDetails = new();
    private IEnumerable<PetDto> _pets;
    private IEnumerable<VisitDto> _visits;
    private readonly IApiService _apiService;
    private readonly IResponseHandler _responseHandler;
    private readonly CustomerDto _customer;

    public CustomerProfileBuilder(IApiService apiService, IResponseHandler responseHandler, CustomerDto customer)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        _customer = customer ?? throw new ArgumentNullException(nameof(customer));
    }

    public ICustomerProfileBuilder WithPets()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _pets = await _responseHandler.EvaluateResponse <IEnumerable<PetDto>>(nameof(_pets), _responseDetails,
                async () =>
                    await _apiService.GetAsync("PetApiClient", $"Api/Pet/Customer/{_customer.Id}")
                        .ConfigureAwait(false)); ;
        }));

        return this;
    }

    public ICustomerProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await _responseHandler.EvaluateResponse<IEnumerable<VisitDto>>(nameof(_visits), _responseDetails,
                async () =>
                    await _apiService.GetAsync("VisitApiClient", $"Api/Visit/Customer/{_customer.Id}")
                        .ConfigureAwait(false)); ;
        }));
        return this;
    }

    public async Task<CustomerProfileDto> BuildAsync()
    {

        while (_builderQueue.TryDequeue(out var currentTask))
        {
            await currentTask.ConfigureAwait(false);
        }

        return new CustomerProfileDto
        {
            Customer = _customer,
            Pets = _pets,
            Visits = _visits,
            ResponseDetails = _responseDetails
        };
    }
}
