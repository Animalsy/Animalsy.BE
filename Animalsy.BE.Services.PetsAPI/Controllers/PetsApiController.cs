using Animalsy.BE.Services.PetsAPI.Models.Dto;
using Animalsy.BE.Services.PetsAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.PetsAPI.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsApiController(IPetRepository petRepository) : ControllerBase
    {
        [HttpGet("GetPets/{customerId}")]
        public async Task<ResponseDto> GetByCustomerAsync([FromRoute] Guid customerId)
        {
            var pets = await petRepository.GetByCustomerAsync(customerId);
            return pets.Any()
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = pets,
                }
                : new ResponseDto
                {
                    Message = "You have not added any pet yet"
                };
        }

        [HttpGet]
        [Route("GetPet/{petId}")]
        public async Task<ResponseDto> GetByIdAsync([FromRoute] Guid petId)
        {
            var pet = await petRepository.GetByIdAsync(petId);
            return pet != null
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = pet,
                }
                : new ResponseDto
                {
                    Message = "Pet with provided Id has not been found"
                };
        }

        [HttpPost("CreatePet")]
        public async Task<ResponseDto> CreateAsync([FromBody] CreatePetDto petDto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var createdPetId = await petRepository.CreateAsync(petDto);
            return new ResponseDto
            {
                IsSuccess = true,
                Result = createdPetId,
                Message = "Pet has been created successfully"
            };
        }

        [HttpPut("UpdatePet/{petId}")]
        public async Task<ResponseDto> UpdateAsync([FromRoute] Guid petId, [FromBody] UpdatePetDto petDto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var updateResult = await petRepository.TryUpdateAsync(petId, petDto);
            return new ResponseDto
            {
                IsSuccess = updateResult,
                Result = petId,
                Message = updateResult ? "Pet has been updated successfully" : "Pet with provided Id has not been found"
            };
        }

        [HttpDelete("DeletePet/{petId}")]
        public async Task<ResponseDto> DeleteAsync([FromRoute] Guid petId)
        {
            var deleteResult = await petRepository.TryDeleteAsync(petId);
            return new ResponseDto
            {
                IsSuccess = deleteResult,
                Result = petId,
                Message = deleteResult ? "Pet has been deleted successfully" : "Pet with provided Id has not been found"
            };
        }
    }
}
