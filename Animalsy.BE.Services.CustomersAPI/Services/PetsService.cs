using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using Animalsy.BE.Services.CustomersAPI.Services.Interfaces;

namespace Animalsy.BE.Services.CustomersAPI.Services
{
    public class PetsService(IHttpClientFactory httpClientFactory) : IPetsService
    {
        public async Task<IEnumerable<PetDto>> GetPetsAsync(Guid customerId)
        {
            using var client = httpClientFactory.CreateClient("PetApiClient");
            var response = await client.GetAsync($"/Api/Pets/Customers/Ids/{customerId}");
            var content = await response.Content.ReadAsStringAsync();

            return new List<PetDto>();
        }
    }
}
