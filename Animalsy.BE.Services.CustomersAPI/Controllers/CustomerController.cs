using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository;
using Animalsy.BE.Services.CustomerAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.CustomerAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class CustomerController: Controller
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CreateCustomerValidator _createCustomerValidator;
    private readonly UpdateCustomerValidator _updateCustomerValidator;
    private readonly UniqueIdValidator _uniqueIdValidator;
    private readonly EmailValidator _emailValidator;

    public CustomerController(EmailValidator emailValidator, UpdateCustomerValidator updateCustomerValidator, CreateCustomerValidator createCustomerValidator,
        ICustomerRepository customerRepository, UniqueIdValidator uniqueIdValidator)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _createCustomerValidator = createCustomerValidator ?? throw new ArgumentNullException(nameof(createCustomerValidator));
        _updateCustomerValidator = updateCustomerValidator ?? throw new ArgumentNullException(nameof(updateCustomerValidator));
        _uniqueIdValidator = uniqueIdValidator ?? throw new ArgumentNullException(nameof(uniqueIdValidator));
        _emailValidator = emailValidator ?? throw new ArgumentNullException(nameof(emailValidator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Any() 
            ? Ok(customers) 
            : NotFound("There are no customers added yet");
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid customerId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var customer = await _customerRepository.GetByIdAsync(customerId);
        return customer != null
            ? Ok(customer)
            : NotFound(CustomerNotFoundMessage("Id", customerId.ToString()));
    }

    [HttpGet("Email/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByEmailAsync([FromRoute] string email)
    {
        var validationRequest = await _emailValidator.ValidateAsync(email);
        if (!validationRequest.IsValid) return BadRequest(validationRequest);
            
        var customer = await _customerRepository.GetByEmailAsync(email);
        return customer != null
            ? Ok(customer)
            : NotFound(CustomerNotFoundMessage("Email", email));
    }

    [HttpGet("Profile/{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerProfileAsync(Guid customerId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var customer = await _customerRepository.GetCustomerProfileAsync(customerId);
        return customer != null
            ? Ok(customer)
            : NotFound(CustomerNotFoundMessage("Id", customerId.ToString()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerDto customerDto)
    {
        var validationResult = await _createCustomerValidator.ValidateAsync(customerDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var existingCustomer = await _customerRepository.GetByEmailAsync(customerDto.EmailAddress);
        if(existingCustomer != null) return Conflict($"Customer with Email {customerDto.EmailAddress} already exists");
            
        var createdCustomerId = await _customerRepository.CreateAsync(customerDto);
        return Ok(createdCustomerId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerDto customerDto)
    {
        var validationResult = await _updateCustomerValidator.ValidateAsync(customerDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await _customerRepository.TryUpdateAsync(customerDto);
        return updateSuccessful
            ? Ok("Customer has been updated successfully")
            : NotFound(CustomerNotFoundMessage("Id", customerDto.Id.ToString()));

    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid customerId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await _customerRepository.TryDeleteAsync(customerId);
        return deleteSuccessful
            ? Ok("Customer has been deleted successfully")
            : NotFound(CustomerNotFoundMessage("Id", customerId.ToString()));
    }

    private static string CustomerNotFoundMessage(string topic, string email) => $"Customer with {topic} {email} has not been found";
}