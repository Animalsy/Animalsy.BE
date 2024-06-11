using Animalsy.BE.Services.AuthAPI.Models;

namespace Animalsy.BE.Services.AuthAPI.Services
{
    public class CustomerService(IHttpClientFactory httpClientFactory) : ICustomerService
    {
        public Task<ResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            throw new NotImplementedException();
        }
    }
}
