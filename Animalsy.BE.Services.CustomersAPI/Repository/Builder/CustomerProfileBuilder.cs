﻿using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Services;

namespace Animalsy.BE.Services.CustomerAPI.Repository.Builder;

public class CustomerProfileBuilder(IApiService apiService, CustomerDto customer) : ICustomerProfileBuilder
{
    private readonly Queue<Task> _builderQueue = new();
    private IEnumerable<PetDto> _pets;
    private IEnumerable<VisitDto> _visits;

    public ICustomerProfileBuilder WithPets()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _pets = await apiService.GetAsync<IEnumerable<PetDto>>("PetApiClient", $"Api/Pet/Customer/{customer.Id}");
        }));

        return this;
    }

    public ICustomerProfileBuilder WithVisits()
    {
        _builderQueue.Enqueue(Task.Run(async () =>
        {
            _visits = await apiService.GetAsync<IEnumerable<VisitDto>>("VisitApiClient", $"Api/Visit/Customer/{customer.Id}");
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
            Id = customer.Id,
            Name = customer.Name,
            City = customer.City,
            Street = customer.Street,
            Building = customer.Building,
            Flat = customer.Flat,
            PostalCode = customer.PostalCode,
            PhoneNumber = customer.PhoneNumber,
            EmailAddress = customer.EmailAddress,
            Pets = _pets,
            Visits = _visits,
        };
    }
}