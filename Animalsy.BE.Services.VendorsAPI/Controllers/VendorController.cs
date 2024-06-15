using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository;
using Animalsy.BE.Services.VendorAPI.Validators.Factory;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.VendorAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class VendorController : Controller
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IValidatorFactory _validatorFactory;

    public VendorController(IVendorRepository vendorRepository, IValidatorFactory validatorFactory)
    {
        _vendorRepository = vendorRepository ?? throw new ArgumentNullException(nameof(vendorRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var vendors = await _vendorRepository.GetAllAsync();
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
        var vendors = await _vendorRepository.GetByNameAsync(name);
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
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(vendorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendor = await _vendorRepository.GetByIdAsync(vendorId);
        return vendor != null
            ? Ok(vendor)
            : NotFound(VendorNotFoundMessage("Id", vendorId.ToString()));
    }

    [HttpGet("Profile/{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVendorProfileAsync(Guid vendorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(vendorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendor = await _vendorRepository.GetVendorProfileAsync(vendorId);
        return vendor != null
            ? Ok(vendor)
            : NotFound(VendorNotFoundMessage("Id", vendorId.ToString()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVendorDto vendorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateVendorDto>()
            .ValidateAsync(vendorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);
            
        var createdVendorId = await _vendorRepository.CreateAsync(vendorDto);
        return Ok(createdVendorId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVendorDto vendorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdateVendorDto>()
            .ValidateAsync(vendorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await _vendorRepository.TryUpdateAsync(vendorDto);
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
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(customerId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await _vendorRepository.TryDeleteAsync(customerId);
        return deleteSuccessful
            ? Ok("Customer has been deleted successfully")
            : NotFound(VendorNotFoundMessage("Id",customerId.ToString()));
    }

    private static string VendorNotFoundMessage(string topic, string value) => $"Vendor with {topic} {value} has not been found";
}