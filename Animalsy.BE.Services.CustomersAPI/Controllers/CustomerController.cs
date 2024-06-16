using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Animalsy.BE.Services.CustomerAPI.Models.Dto;
using Animalsy.BE.Services.CustomerAPI.Repository;
using Animalsy.BE.Services.CustomerAPI.Utilities;
using Animalsy.BE.Services.CustomerAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = SD.RoleAdmin)]
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
    [Authorize]
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
    [Authorize]
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

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCustomerDto customerDto)
    {
        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), customerDto.UserId) || User.IsInRole(SD.RoleAdmin)) 
            return Unauthorized();

        var validationResult = await _validatorFactory.GetValidator<UpdateCustomerDto>()
            .ValidateAsync(customerDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await _customerRepository.TryUpdateAsync(customerDto);
        return updateSuccessful
            ? Ok("Customer has been updated successfully")
            : NotFound(CustomerNotFoundMessage(customerDto.UserId.ToString()));

    }

    [HttpDelete("{userId:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid userId)
    {
        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), userId) || User.IsInRole(SD.RoleAdmin))
            return Unauthorized();

        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(userId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await _customerRepository.TryDeleteAsync(userId);
        return deleteSuccessful
            ? Ok("Customer has been deleted successfully")
            : NotFound(CustomerNotFoundMessage(userId.ToString()));
    }

    private static bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId;
    }

    private static string CustomerNotFoundMessage(string id) => $"Customer with Id {id} has not been found";
}