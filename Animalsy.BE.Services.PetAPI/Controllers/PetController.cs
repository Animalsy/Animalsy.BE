using Animalsy.BE.Services.PetAPI.Models.Dto;
using Animalsy.BE.Services.PetAPI.Repository;
using Animalsy.BE.Services.PetAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Animalsy.BE.Services.PetAPI.Utilities;

namespace Animalsy.BE.Services.PetAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class PetController: ControllerBase
{
    private readonly IPetRepository _petRepository;
    private readonly IValidatorFactory _validatorFactory;

    public PetController(IPetRepository petRepository, IValidatorFactory validatorFactory)
    {
        _petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }

    [HttpGet("User/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCustomerAsync([FromRoute] Guid userId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(userId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), userId))
            return Unauthorized();

        var pets = await _petRepository.GetByUserAsync(userId);
        return pets.Any()
            ? Ok(pets)
            : NotFound("You have not added any pet yet");
    }

    [HttpGet]
    [Route("{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid petId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(petId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var pet = await _petRepository.GetByIdAsync(petId);
        return pet != null
            ? Ok(pet)
            : NotFound(PetIdNotFoundMessage(petId));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePetDto petDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreatePetDto>()
            .ValidateAsync(petDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), petDto.UserId))
            return Unauthorized();

        var createdPetId = await _petRepository.CreateAsync(petDto);
        return new ObjectResult(createdPetId) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdatePetDto petDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdatePetDto>()
            .ValidateAsync(petDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), petDto.UserId))
            return Unauthorized();

        var updateResult = await _petRepository.TryUpdateAsync(petDto);
        return updateResult
            ? Ok("Pet has been updated successfully")
            : NotFound(PetIdNotFoundMessage(petDto.Id));
    }

    [HttpDelete("{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid petId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(petId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var petDto = await _petRepository.GetByIdAsync(petId);
        if (petDto == null) return NotFound(PetIdNotFoundMessage(petId));

        if (!CheckLoggedUser(User.FindFirst(ClaimTypes.NameIdentifier), petDto.UserId))
            return Unauthorized();

        var deleteResult = await _petRepository.TryDeleteAsync(petId);
        return deleteResult 
            ? Ok("Pet has been deleted successfully")
            : NotFound(PetIdNotFoundMessage(petId));
    }

    private bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return (claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId) || User.IsInRole(SD.RoleAdmin);
    }

    private static string PetIdNotFoundMessage(Guid? id) => $"Pet with Id {id} has not been found";
}