using Animalsy.BE.Services.AuthAPI.Models.Dto;

namespace Animalsy.BE.Services.AuthAPI.Services;

public interface ICustomerService
{
    Task<ResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto);
}