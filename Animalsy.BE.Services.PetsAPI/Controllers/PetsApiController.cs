using Animalsy.BE.Services.PetsAPI.Models.Dto;
using Animalsy.BE.Services.PetsAPI.Repository;
using Animalsy.BE.Services.PetsAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.PetsAPI.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsApiController(IPetRepository petRepository, UniqueIdValidator idValidator, CreatePetValidator createPetValidator,
        UpdatePetValidator updatePetValidator) : ControllerBase
    {
        [HttpGet("GetPets/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCustomerAsync([FromRoute] Guid customerId)
        {
            var validationResult = await idValidator.ValidateAsync(customerId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var pets = await petRepository.GetByCustomerAsync(customerId);
            return pets.Any()
                ? Ok(pets)
                : NotFound("You have not added any pet yet");
        }

        [HttpGet]
        [Route("GetPet/{petId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid petId)
        {
            var validationResult = await idValidator.ValidateAsync(petId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var pet = await petRepository.GetByIdAsync(petId);
            return pet != null
                ? Ok(pet)
                : NotFound(PetIdNotFoundMessage(petId));
        }

        [HttpPost("CreatePet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePetDto petDto)
        {
            var validationResult = await createPetValidator.ValidateAsync(petDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var createdPetId = await petRepository.CreateAsync(petDto);
            return Ok(createdPetId);
        }

        [HttpPut("UpdatePet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePetDto petDto)
        {
            var validationResult = await updatePetValidator.ValidateAsync(petDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var updateResult = await petRepository.TryUpdateAsync(petDto);
            return updateResult
                ? Ok("Pet has been updated successfully")
                : NotFound(PetIdNotFoundMessage(petDto.Id));
        }

        [HttpDelete("DeletePet/{petId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid petId)
        {
            var validationResult = await idValidator.ValidateAsync(petId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var deleteResult = await petRepository.TryDeleteAsync(petId);
            return deleteResult
                ? Ok("Pet has been deleted successfully")
                : NotFound(PetIdNotFoundMessage(petId));
        }

        private static string PetIdNotFoundMessage(Guid? id) => $"Pet with Id {id} has not been found";
    }
}
