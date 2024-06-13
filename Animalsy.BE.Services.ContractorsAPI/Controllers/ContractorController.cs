using Animalsy.BE.Services.ContractorAPI.Models.Dto;
using Animalsy.BE.Services.ContractorAPI.Repository;
using Animalsy.BE.Services.ContractorAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.ContractorAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ContractorController : ControllerBase
{
    private readonly IContractorRepository _contractorRepository;
    private readonly UniqueIdValidator _idValidator;
    private readonly CreateContractorValidator _createContractorValidator;
    private readonly UpdateContractorValidator _updateContractorValidator;

    public ContractorController(IContractorRepository contractorRepository, UniqueIdValidator idValidator,
        CreateContractorValidator createContractorValidator, UpdateContractorValidator updateContractorValidator)
    {
        _contractorRepository = contractorRepository ?? throw new ArgumentNullException(nameof(contractorRepository));
        _idValidator = idValidator ?? throw new ArgumentNullException(nameof(idValidator));
        _createContractorValidator = createContractorValidator ?? throw new ArgumentNullException(nameof(createContractorValidator));
        _updateContractorValidator = updateContractorValidator ?? throw new ArgumentNullException(nameof(updateContractorValidator));
    }


    [HttpGet("{contractorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid contractorId)
    {
        var validationResult = await _idValidator.ValidateAsync(contractorId);
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
        var validationResult = await _idValidator.ValidateAsync(vendorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var contractors = await _contractorRepository.GetByVendorAsync(vendorId);
        return contractors.Any()
            ? Ok(contractors)
            : NotFound(VendorIdNotFoundMessage(vendorId));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateContractorDto contractorDto)
    {
        var validationResult = await _createContractorValidator.ValidateAsync(contractorDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdContractorId = await _contractorRepository.CreateAsync(contractorDto);
        return Ok(createdContractorId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateContractorDto contractorDto)
    {
        var validationResult = await _updateContractorValidator.ValidateAsync(contractorDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateResult = await _contractorRepository.TryUpdateAsync(contractorDto);
        return updateResult
            ? Ok("Contractor has been updated successfully")
            : NotFound(ContractorIdNotFoundMessage(contractorDto.Id));
    }

    [HttpDelete("{contractorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid contractorId)
    {
        var validationResult = await _idValidator.ValidateAsync(contractorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteResult = await _contractorRepository.TryDeleteAsync(contractorId);
        return deleteResult
            ? Ok("Contractor has been deleted successfully")
            : NotFound(ContractorIdNotFoundMessage(contractorId));
    }

    private static string ContractorIdNotFoundMessage(Guid? id) => $"Contractor with Id {id} has not been found";
    private static string VendorIdNotFoundMessage(Guid? id) => $"Contractors for Vendor Id {id} have not been found";
}