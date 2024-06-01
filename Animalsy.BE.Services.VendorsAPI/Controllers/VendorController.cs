using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository;
using Animalsy.BE.Services.VendorAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.VendorAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class VendorController(IVendorRepository vendorRepository, CreateVendorValidator createVendorValidator,
    UpdateVendorValidator updateVendorValidator, UniqueIdValidator idValidator, EmailValidator emailValidator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var vendors = await vendorRepository.GetAllAsync();
        return vendors.Any() 
            ? Ok(vendors) 
            : NotFound("There are no vendors added yet");
    }

    [HttpGet("Name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var vendors = await vendorRepository.GetByNameAsync(name);
        return vendors.Any()
            ? Ok(vendors)
            : NotFound("There are no vendors with provided name");
    }

    [HttpGet("{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid vendorId)
    {
        var validationResult = await idValidator.ValidateAsync(vendorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendor = await vendorRepository.GetByIdAsync(vendorId);
        return vendor != null
            ? Ok(vendor)
            : NotFound(VendorNotFoundMessage("Id", vendorId.ToString()));
    }

    [HttpGet("Email/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByEmailAsync([FromRoute] string email)
    {
        var validationRequest = await emailValidator.ValidateAsync(email);
        if (!validationRequest.IsValid) return BadRequest(validationRequest);
            
        var vendor = await vendorRepository.GetByEmailAsync(email);
        return vendor != null
            ? Ok(vendor)
            : NotFound(VendorNotFoundMessage("Email", email));
    }

    [HttpGet("Profile/{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVendorProfileAsync(Guid vendorId)
    {
        var validationResult = await idValidator.ValidateAsync(vendorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendor = await vendorRepository.GetVendorProfileAsync(vendorId);
        return vendor != null
            ? Ok(vendor)
            : NotFound(VendorNotFoundMessage("Id", vendorId.ToString()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVendorDto vendorDto)
    {
        var validationResult = await createVendorValidator.ValidateAsync(vendorDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var existingVendor = await vendorRepository.GetByEmailAsync(vendorDto.EmailAddress);
        if(existingVendor != null) return Conflict($"Vendor with Email '{vendorDto.EmailAddress}' already exists");
            
        var createdVendorId = await vendorRepository.CreateAsync(vendorDto);
        return Ok(createdVendorId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVendorDto vendorDto)
    {
        var validationResult = await updateVendorValidator.ValidateAsync(vendorDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await vendorRepository.TryUpdateAsync(vendorDto);
        return updateSuccessful
            ? Ok("Vendor has been updated successfully")
            : NotFound(VendorNotFoundMessage("Id",vendorDto.Id.ToString()));

    }

    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid customerId)
    {
        var validationResult = await idValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await vendorRepository.TryDeleteAsync(customerId);
        return deleteSuccessful
            ? Ok("Customer has been deleted successfully")
            : NotFound(VendorNotFoundMessage("Id",customerId.ToString()));
    }

    private static string VendorNotFoundMessage(string topic, string value) => $"Vendor with {topic} {value} has not been found";
}