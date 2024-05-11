using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using Animalsy.BE.Services.CustomersAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.CustomersAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersApiController(ICustomerRepository customerRepository) : Controller
    {
        [HttpGet("GetCustomers")]
        public async Task<ResponseDto> GetAllAsync()
        {
            var customers = await customerRepository.GetAllAsync();
            return new ResponseDto
            {
                IsSuccess = true,
                Result = customers,
            };
        }

        [HttpGet("GetCustomer/{customerId}")]
        public async Task<ResponseDto> GetAllAsync([FromRoute] Guid customerId)
        {
            var customer = await customerRepository.GetByIdAsync(customerId);
            return customer != null
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = customer
                }
                : new ResponseDto
                {
                    Message = "Customer with provided Id has not been found"
                };
        }

        [HttpPost("CreateCustomer")]
        public async Task<ResponseDto> CreateAsync([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var createdCustomerId = await customerRepository.CreateAsync(dto);
            return new ResponseDto
            {
                IsSuccess = true,
                Result = createdCustomerId,
                Message = "Customer has been created successfully"
            };
        }

        [HttpPut("UpdateCustomer/{customerId}")]
        public async Task<ResponseDto> UpdateAsync([FromRoute] Guid customerId, [FromBody] UpdateCustomerDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var updateResult = await customerRepository.TryUpdateAsync(customerId, dto);
            return new ResponseDto
            {
                IsSuccess = updateResult,
                Result = customerId,
                Message = updateResult ? "Customer has been updated successfully" : "Customer with provided Id has not been found"
            };
        }

        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<ResponseDto> DeleteAsync([FromRoute] Guid customerId)
        {
            var deleteResult = await customerRepository.TryDeleteAsync(customerId);
            return new ResponseDto
            {
                IsSuccess = deleteResult,
                Result = customerId,
                Message = deleteResult ? "Customer has been deleted successfully" : "Customer with provided Id has not been found"
            };
        }

    }
}
