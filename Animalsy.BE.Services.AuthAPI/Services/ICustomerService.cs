using Animalsy.BE.Services.AuthAPI.Models;

namespace Animalsy.BE.Services.AuthAPI.Services
{
    public interface ICustomerService
    {
        Task<ResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto);
    }
}
