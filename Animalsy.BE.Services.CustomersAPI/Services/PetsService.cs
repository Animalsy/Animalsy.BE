using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Services.Interfaces;

namespace Animalsy.BE.Services.CustomerAPI.Services
{
    public class PetsService(IHttpClientFactory httpClientFactory) : IPetsService
    {
        public async Task<IEnumerable<PetDto>> GetPetsAsync(Guid customerId)
        {
            using var client = httpClientFactory.CreateClient("PetApiClient");
            var response = await client.GetAsync($"/Api/Pet/Customer/{customerId}");
            var content = await response.Content.ReadAsStringAsync();

            return new List<PetDto>();
        }
    }
}
