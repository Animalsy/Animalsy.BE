using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using Animalsy.BE.Services.ContractorAPI.Repository;
using Animalsy.BE.Services.ContractorAPI.Utilities;
using Animalsy.BE.Services.ContractorAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Animalsy.BE.Services.ContractorAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ContractorController : ControllerBase
{
    private readonly IContractorRepository _contractorRepository;
    private readonly IValidatorFactory _validatorFactory;

    public ContractorController(IContractorRepository contractorRepository, IValidatorFactory validatorFactory)
    {
        _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }


    [HttpGet("{contractorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid contractorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(contractorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var contractor = await _contractorRepository.GetByIdAsync(contractorId);
        return contractor != null
            ? Ok(contractor)
            : NotFound(ContractorIdNotFoundMessage(contractorId));
    }

    [HttpGet("Vendor/{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByVendorAsync([FromRoute] Guid vendorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(vendorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var contractors = await _contractorRepository.GetByVendorAsync(vendorId);
        return contractors.Any()
            ? Ok(contractors)
            : NotFound(VendorIdNotFoundMessage(vendorId));
    }

    [HttpPost] 
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateContractorDto contractorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateContractorDto>()
            .ValidateAsync(contractorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdContractorId = await _contractorRepository.CreateAsync(contractorDto);
        return Ok(createdContractorId);
    }

    [HttpPut]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateContractorDto updateContractorDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdateContractorDto>()
            .ValidateAsync(updateContractorDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), updateContractorDto.UserId))
            return Unauthorized();

        var updateResult = await _contractorRepository.TryUpdateAsync(updateContractorDto);
        return updateResult
            ? Ok("Contractor has been updated successfully")
            : NotFound(ContractorIdNotFoundMessage(updateContractorDto.Id));
    }

    [HttpDelete("{contractorId:guid}")]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid contractorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(contractorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var contractorDto = await _contractorRepository.GetByIdAsync(contractorId);
        if (contractorDto == null) return NotFound(ContractorIdNotFoundMessage(contractorId));

        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), contractorDto.UserId))
            return Unauthorized();

        await _contractorRepository.DeleteAsync(contractorDto);
        return Ok("Contractor has been deleted successfully");
    }

    private bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return (claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId) || User.IsInRole(SD.RoleAdmin);
    }

    private static string ContractorIdNotFoundMessage(Guid? id) => $"Contractor with Id {id} has not been found";
    private static string VendorIdNotFoundMessage(Guid? id) => $"Contractors for Vendor Id {id} have not been found";
}