using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Repository;
using Animalsy.BE.Services.VisitAPI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.VisitAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class VisitController(
    IVisitRepository visitRepository,
    CreateVisitValidator createVisitValidator,
    UpdateVisitValidator updateVisitValidator,
    UniqueIdValidator idValidator) : Controller
{

    [HttpGet("{visitId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid visitId)
    {
        var validationResult = await idValidator.ValidateAsync(visitId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var visits = await visitRepository.GetByIdAsync(visitId);
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
        var validationResult = await idValidator.ValidateAsync(vendorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var results = await visitRepository.GetByVendorIdAsync(vendorId);
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
        var validationResult = await idValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var visit = await visitRepository.GetByCustomerIdAsync(customerId);
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
        var validationResult = await createVisitValidator.ValidateAsync(visitDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdVisitId = await visitRepository.CreateAsync(visitDto);
        return Ok(createdVisitId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVisitDto visitDto)
    {
        var validationResult = await updateVisitValidator.ValidateAsync(visitDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateSuccessful = await visitRepository.TryUpdateAsync(visitDto);
        return updateSuccessful
            ? Ok("Visit has been updated successfully")
            : NotFound(VisitNotFoundMessage("Id", visitDto.Id.ToString()));

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

        var deleteSuccessful = await visitRepository.TryDeleteAsync(customerId);
        return deleteSuccessful
            ? Ok("Visit has been deleted successfully")
            : NotFound(VisitNotFoundMessage("Id", customerId.ToString()));
    }

    private static string VisitNotFoundMessage(string topic, string value) =>
        $"Visit with provided {topic}: {value} has not been found";

    private static string VisitsNotFoundMessage(string topic, string value) =>
        $"Visits with provided {topic}: {value} have not been found";
}