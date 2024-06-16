using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Animalsy.BE.Services.VendorAPI.Repository;
using Animalsy.BE.Services.VendorAPI.Utilities;
using Animalsy.BE.Services.VendorAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Animalsy.BE.Services.VendorAPI.Services;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Animalsy.BE.Services.VendorAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class VendorController : Controller
{
    private readonly IVendorRepository _vendorRepository;
    private readonly IValidatorFactory _validatorFactory;
    private readonly IAuthService _authService;

    public VendorController(IVendorRepository vendorRepository, IValidatorFactory validatorFactory, IAuthService authService)
    {
        _vendorRepository = vendorRepository ?? throw new ArgumentNullException(nameof(vendorRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
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

    [HttpGet("Profiles/{userId:guid}")]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVendorProfilesAsync(Guid userId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(userId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var profiles = await _vendorRepository.GetVendorProfilesAsync(userId);
        return !profiles.IsNullOrEmpty()
            ? Ok(profiles)
            : NotFound($"Vendors for UserId :{userId} have not been found");
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVendorDto createVendorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateVendorDto>()
            .ValidateAsync(createVendorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);
            
        var createdVendorId = await _vendorRepository.CreateAsync(createVendorDto);
        await AssignVendorRoleIfRequiredAsync().ConfigureAwait(false);
        
        return new ObjectResult(createdVendorId) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVendorDto updateVendorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdateVendorDto>()
            .ValidateAsync(updateVendorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), updateVendorDto.UserId))
            return Unauthorized();

        var updateSuccessful = await _vendorRepository.TryUpdateAsync(updateVendorDto);
        return updateSuccessful
            ? Ok("Vendor has been updated successfully")
            : NotFound(VendorNotFoundMessage("Id",updateVendorDto.Id.ToString()));
    }

    [HttpDelete("{vendorId:guid}")]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid vendorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(vendorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendorDto = await _vendorRepository.GetByIdAsync(vendorId);
        if (vendorDto == null) return NotFound(VendorNotFoundMessage("Id", vendorId.ToString()));

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), vendorDto.UserId))
            return Unauthorized();

        await _vendorRepository.DeleteAsync(vendorDto);
        return Ok("Customer has been deleted successfully");
    }

    private async Task AssignVendorRoleIfRequiredAsync()
    {
        var email = User.FindFirst(ClaimTypes.Email);

        if (User.IsInRole(SD.RoleVendor) || email == null)
            return;

        await _authService.AssignRoleAsync(new AssignRoleDto { Email = email.Value, RoleName = SD.RoleVendor });
    }

    private bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return (claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId) || User.IsInRole(SD.RoleAdmin);
    }

    private static string VendorNotFoundMessage(string topic, string value) => $"Vendor with {topic} {value} has not been found";
}