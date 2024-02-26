using Animalsy.BE.Services.PetAPI.Models;
using Animalsy.BE.Services.PetsAPI.Models.Dto;
using Animalsy.BE.Services.PetsAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.PetsAPI.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPetRepository _petRepository;

        public PetsApiController(IMapper mapper, IPetRepository petRepository)
        {
            _mapper = mapper;
            _petRepository = petRepository;
        }

        [HttpGet("GetPets/{customerId}")]
        public async Task<ResponseDto> GetByCustomerAsync([FromRoute] Guid customerId)
        {
            var pets = await _petRepository.GetByCustomerAsync(customerId);
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
            var pet = await _petRepository.GetByIdAsync(petId);
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
        public async Task<ResponseDto> CreateAsync([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var createdPetId = await _petRepository.CreateAsync(_mapper.Map<Pet>(dto));
            return new ResponseDto
            {
                IsSuccess = true,
                Result = createdPetId,
                Message = "Pet has been created successfully"
            };
        }

        [HttpPut("UpdatePet/{petId}")]
        public async Task<ResponseDto> UpdateAsync([FromRoute] Guid petId, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var updateResult = await _petRepository.TryUpdateAsync(petId, _mapper.Map<Pet>(dto));
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
            var deleteResult = await _petRepository.TryDeleteAsync(petId);
            return new ResponseDto
            {
                IsSuccess = deleteResult,
                Result = petId,
                Message = deleteResult ? "Pet has been deleted successfully" : "Pet with provided Id has not been found"
            };
        }
    }
}
