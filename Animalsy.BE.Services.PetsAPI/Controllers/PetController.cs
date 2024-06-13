using Animalsy.BE.Services.PetAPI.Models.Dto;
using Animalsy.BE.Services.PetAPI.Repository;
using Animalsy.BE.Services.PetAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.PetAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class PetController: ControllerBase
{
    private readonly IPetRepository _petRepository;
    private readonly UniqueIdValidator _uniqueIdValidator;
    private readonly CreatePetValidator _createPetValidator;
    private readonly UpdatePetValidator _updatePetValidator;

    public PetController(IPetRepository petRepository, UniqueIdValidator idValidator, CreatePetValidator createPetValidator,
        UpdatePetValidator updatePetValidator)
    {
        _petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
        _uniqueIdValidator = idValidator ?? throw new ArgumentNullException(nameof(idValidator));
        _createPetValidator = createPetValidator ?? throw new ArgumentNullException(nameof(createPetValidator));
        _updatePetValidator = updatePetValidator ?? throw new ArgumentNullException(nameof(updatePetValidator));
    }


    [HttpGet("Customer/{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCustomerAsync([FromRoute] Guid customerId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(customerId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var pets = await _petRepository.GetByCustomerAsync(customerId);
        return pets.Any()
            ? Ok(pets)
            : NotFound("You have not added any pet yet");
    }

    [HttpGet]
    [Route("{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid petId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(petId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var pet = await _petRepository.GetByIdAsync(petId);
        return pet != null
            ? Ok(pet)
            : NotFound(PetIdNotFoundMessage(petId));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePetDto petDto)
    {
        var validationResult = await _createPetValidator.ValidateAsync(petDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdPetId = await _petRepository.CreateAsync(petDto);
        return Ok(createdPetId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdatePetDto petDto)
    {
        var validationResult = await _updatePetValidator.ValidateAsync(petDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateResult = await _petRepository.TryUpdateAsync(petDto);
        return updateResult
            ? Ok("Pet has been updated successfully")
            : NotFound(PetIdNotFoundMessage(petDto.Id));
    }

    [HttpDelete("{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid petId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(petId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteResult = await _petRepository.TryDeleteAsync(petId);
        return deleteResult
            ? Ok("Pet has been deleted successfully")
            : NotFound(PetIdNotFoundMessage(petId));
    }

    private static string PetIdNotFoundMessage(Guid? id) => $"Pet with Id {id} has not been found";
}