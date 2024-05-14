using Animalsy.BE.Services.CustomersAPI.Models.Dto;
using Animalsy.BE.Services.CustomersAPI.Repository;
using Animalsy.BE.Services.CustomersAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.CustomersAPI.Controllers
{
    [Route("Api/Customers")]
    [ApiController]
    public class CustomersApiController(ICustomerRepository customerRepository, CreateCustomerValidator createCustomerValidator,
        UpdateCustomerValidator updateCustomerValidator, UniqueIdValidator idValidator, EmailValidator emailValidator) : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await customerRepository.GetAllAsync();
            return customers.Any() 
                ? Ok(customers) 
                : NotFound("There are no customers added yet");
        }

        [HttpGet("Ids/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid customerId)
        {
            var validationResult = await idValidator.ValidateAsync(customerId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var customer = await customerRepository.GetByIdAsync(customerId);
            return customer != null
                ? Ok(customer)
                : NotFound(CustomerIdNotFoundMessage(customerId));
        }

        [HttpGet("Emails/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByEmailAsync([FromRoute] string email)
        {
            var validationRequest = await emailValidator.ValidateAsync(email);
            if (!validationRequest.IsValid) return BadRequest(validationRequest);
            
            var customer = await customerRepository.GetByEmailAsync(email);
            return customer != null
                ? Ok(customer)
                : NotFound(CustomerEmailNotFoundMessage(email));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerDto customerDto)
        {
            var validationResult = await createCustomerValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var existingCustomer = await customerRepository.GetByEmailAsync(customerDto.EmailAddress);
            if(existingCustomer != null) return Conflict(CustomerEmailNotFoundMessage(customerDto.EmailAddress));
            
            var createdCustomerId = await customerRepository.CreateAsync(customerDto);
            return Ok(createdCustomerId);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerDto customerDto)
        {
            var validationResult = await updateCustomerValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var updateSuccessful = await customerRepository.TryUpdateAsync(customerDto);
            return updateSuccessful
                ? Ok("Customer has been updated successfully")
                : NotFound(CustomerIdNotFoundMessage(customerDto.Id));

        }

        [HttpDelete("Ids/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid customerId)
        {
            var validationResult = await idValidator.ValidateAsync(customerId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var deleteSuccessful = await customerRepository.TryDeleteAsync(customerId);
            return deleteSuccessful
                ? Ok("Customer has been deleted successfully")
                : NotFound(CustomerIdNotFoundMessage(customerId));
        }

        private static string CustomerIdNotFoundMessage(Guid? id) => $"Customer with Id {id} has not been found";
        private static string CustomerEmailNotFoundMessage(string email) => $"Customer with Email {email} has not been found";
    }
}
