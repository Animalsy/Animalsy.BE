using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Services;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder;

public class CustomerProfileBuilder : ICustomerProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private IEnumerable<PetDto> _pets;
    private IEnumerable<VisitDto> _visits;
    private readonly IApiService _apiService;
    private readonly CustomerDto _customer;

    public CustomerProfileBuilder(IApiService apiService, CustomerDto customer)
    {
        _apiService = apiService;
        _customer = customer;
    }

    public ICustomerProfileBuilder WithPets()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _pets = await _apiService.GetAsync<IEnumerable<PetDto>>("PetApiClient", $"Api/Pet/Customer/{_customer.Id}");
        }));

        return this;
    }

    public ICustomerProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await _apiService.GetAsync<IEnumerable<VisitDto>>("VisitApiClient", $"Api/Visit/Customer/{_customer.Id}");
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
        };
    }
}
