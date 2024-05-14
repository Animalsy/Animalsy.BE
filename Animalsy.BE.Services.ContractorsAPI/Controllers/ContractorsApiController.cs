using Animalsy.BE.Services.ContractorsAPI.Models.Dto;
using Animalsy.BE.Services.ContractorsAPI.Repository;
using Animalsy.BE.Services.ContractorsAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.ContractorsAPI.Controllers
{
    [Route("Api/Contractors")]
    [ApiController]
    public class ProductsApiController(IContractorRepository contractorRepository, UniqueIdValidator idValidator,
        CreateContractorValidator createContractorValidator, UpdateContractorValidator updateContractorValidator) : ControllerBase
    {
        [HttpGet("Ids/{contractorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid contractorId)
        {
            var validationResult = await idValidator.ValidateAsync(contractorId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var contractor = await contractorRepository.GetByIdAsync(contractorId);
            return contractor != null
                ? Ok(contractor)
                : NotFound(ContractorIdNotFoundMessage(contractorId));
        }

        [HttpGet("Vendors/Ids/{vendorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByVendorAsync([FromRoute] Guid vendorId)
        {
            var validationResult = await idValidator.ValidateAsync(vendorId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var contractors = await contractorRepository.GetByVendorAsync(vendorId);
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
            var validationResult = await createContractorValidator.ValidateAsync(contractorDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var createdContractorId = await contractorRepository.CreateAsync(contractorDto);
            return Ok(createdContractorId);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateContractorDto contractorDto)
        {
            var validationResult = await updateContractorValidator.ValidateAsync(contractorDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var updateResult = await contractorRepository.TryUpdateAsync(contractorDto);
            return updateResult
                ? Ok("Contractor has been updated successfully")
                : NotFound(ContractorIdNotFoundMessage(contractorDto.Id));
        }

        [HttpDelete("Ids/{contractorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid contractorId)
        {
            var validationResult = await idValidator.ValidateAsync(contractorId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var deleteResult = await contractorRepository.TryDeleteAsync(contractorId);
            return deleteResult
                ? Ok("Contractor has been deleted successfully")
                : NotFound(ContractorIdNotFoundMessage(contractorId));
        }

        private static string ContractorIdNotFoundMessage(Guid? id) => $"Contractor with Id {id} has not been found";
        private static string VendorIdNotFoundMessage(Guid? id) => $"Vendor with Id {id} has not been found";
    }
}
