using Animalsy.BE.Services.CustomersAPI.Models;
using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using Animalsy.BE.Services.CustomersAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.CustomersAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersApiController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersApiController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }


        [HttpGet("GetCustomers")]
        public async Task<ResponseDto> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return new ResponseDto
            {
                IsSuccess = true,
                Result = customers,
            };
        }

        [HttpGet("GetCustomer/{customerId}")]
        public async Task<ResponseDto> GetAllAsync([FromRoute] Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
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

            var createdCustomerId = await _customerRepository.CreateAsync(_mapper.Map<Customer>(dto));
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

            var updateResult = await _customerRepository.TryUpdateAsync(customerId, _mapper.Map<Customer>(dto));
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
            var deleteResult = await _customerRepository.TryDeleteAsync(customerId);
            return new ResponseDto
            {
                IsSuccess = deleteResult,
                Result = customerId,
                Message = deleteResult ? "Customer has been deleted successfully" : "Customer with provided Id has not been found"
            };
        }

    }
}
