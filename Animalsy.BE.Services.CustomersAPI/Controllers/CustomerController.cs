using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository;
using Animalsy.BE.Services.CustomerAPI.Validators.Factory;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.CustomerAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class CustomerController: Controller
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidatorFactory _validatorFactory;

    public CustomerController(ICustomerRepository customerRepository, IValidatorFactory validatorFactory)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
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
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(customerId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var customer = await _customerRepository.GetByIdAsync(customerId);
        return customer != null
            ? Ok(customer)
            : NotFound(CustomerNotFoundMessage(customerId.ToString()));
    }

    [HttpGet("Profile/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerProfileAsync(Guid userId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(userId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var customer = await _customerRepository.GetCustomerProfileAsync(userId);
        return customer != null
            ? Ok(customer)
            : NotFound(CustomerNotFoundMessage(userId.ToString()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerDto customerDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateCustomerDto>()
            .ValidateAsync(customerDto);

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
        var validationResult = await _validatorFactory.GetValidator<UpdateCustomerDto>()
            .ValidateAsync(customerDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await _customerRepository.TryUpdateAsync(customerDto);
        return updateSuccessful
            ? Ok("Customer has been updated successfully")
            : NotFound(CustomerNotFoundMessage(customerDto.Id.ToString()));

    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid customerId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(customerId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await _customerRepository.TryDeleteAsync(customerId);
        return deleteSuccessful
            ? Ok("Customer has been deleted successfully")
            : NotFound(CustomerNotFoundMessage(customerId.ToString()));
    }

    private static string CustomerNotFoundMessage(string id) => $"Customer with Id {id} has not been found";
}