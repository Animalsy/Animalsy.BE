using System.Security.Claims;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Repository;
using Animalsy.BE.Services.VisitAPI.Utilities;
using Animalsy.BE.Services.VisitAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.VisitAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class VisitController : Controller
{
    private readonly IVisitRepository _visitRepository;
    private readonly IValidatorFactory _validatorFactory;

    public VisitController(IVisitRepository visitRepository, IValidatorFactory validatorFactory)
    {
        _visitRepository = visitRepository ?? throw new ArgumentNullException(nameof(visitRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }

    [HttpGet("{visitId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid visitId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(visitId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var visits = await _visitRepository.GetByIdAsync(visitId);
        return visits != null
            ? Ok(visits)
            : NotFound(VisitNotFoundMessage("Id", visitId.ToString()));
    }

    [HttpGet("Vendor/{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByVendorIdAsync(Guid vendorId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(vendorId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var results = await _visitRepository.GetByVendorIdAsync(vendorId);
        return !results.IsNullOrEmpty()
            ? Ok(results)
            : NotFound(VisitsNotFoundMessage("vendorId", vendorId.ToString()));
    }

    [HttpGet("Customer/{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCustomerIdAsync(Guid customerId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(customerId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var visit = await _visitRepository.GetByCustomerIdAsync(customerId);
        return visit != null
            ? Ok(visit)
            : NotFound(VisitsNotFoundMessage("customerId", customerId.ToString()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVisitDto visitDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateVisitDto>()
            .ValidateAsync(visitDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdVisitId = await _visitRepository.CreateAsync(visitDto);
        return Ok(createdVisitId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVisitDto visitDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdateVisitDto>()
            .ValidateAsync(visitDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await _visitRepository.TryUpdateAsync(visitDto);
        return updateSuccessful
            ? Ok("Visit has been updated successfully")
            : NotFound(VisitNotFoundMessage("Id", visitDto.Id.ToString()));

    }

    [HttpDelete("{visitId:guid}")]
    [Authorize(Roles = SD.RoleAdmin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid visitId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(visitId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteSuccessful = await _visitRepository.TryDeleteAsync(visitId);
        return deleteSuccessful
            ? Ok("Visit has been deleted successfully")
            : NotFound(VisitNotFoundMessage("Id", visitId.ToString()));
    }

    private bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return (claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId) || User.IsInRole(SD.RoleAdmin);
    }

    private static string VisitNotFoundMessage(string topic, string value) =>
        $"Visit with provided {topic}: {value} has not been found";

    private static string VisitsNotFoundMessage(string topic, string value) =>
        $"Visits with provided {topic}: {value} have not been found";
}