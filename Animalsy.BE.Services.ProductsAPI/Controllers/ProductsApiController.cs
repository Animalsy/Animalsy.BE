using Animalsy.BE.Services.ProductsAPI.Models;
using Animalsy.BE.Services.ProductsAPI.Models.Dto;
using Animalsy.BE.Services.ProductsAPI.Repository;
using Animalsy.BE.Services.ProductsAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.ProductsAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController(IProductRepository productRepository, UniqueIdValidator idValidator, 
        CreateProductValidator createProductValidator, UpdateProductValidator updateProductValidator) : ControllerBase
    {
        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            return products.Any()
                ? Ok(products)
                : NotFound("There were no products added yet");
        }

        [HttpGet("GetProductByVendor/{vendorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByVendorAsync([FromRoute] Guid vendorId)
        {
            var validationResult = await idValidator.ValidateAsync(vendorId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var products = await productRepository.GetByVendorAsync(vendorId);
            return products.Any()
                ? Ok(products)
                : NotFound(VendorIdNotFoundMessage(vendorId));
        }

        [HttpGet("GetProductById/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid productId)
        {
            var validationResult = await idValidator.ValidateAsync(productId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var product = await productRepository.GetByIdAsync(productId);
            return product != null
                ? Ok(product)
                : NotFound(ProductIdNotFoundMessage(productId));
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto productDto)
        {
            var validationResult = await createProductValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var createdProductId = await productRepository.CreateAsync(productDto);
            return Ok(createdProductId);
        }

        [HttpPost("UpdateProduct/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid productId,[FromBody] UpdateProductDto productDto)
        {
            var validationResult = await updateProductValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var updateResult = await productRepository.TryUpdateAsync(productDto);
            return updateResult
                ? Ok("Product has been updated successfully")
                : NotFound(ProductIdNotFoundMessage(productDto.Id));
        }

        [HttpGet("DeleteProduct/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid productId)
        {
            var validationResult = await idValidator.ValidateAsync(productId);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var deleteResult = await productRepository.TryDeleteAsync(productId);
            return deleteResult
                ? Ok("Product has been deleted successfully")
                : NotFound(ProductIdNotFoundMessage(productId));
        }

        private static string ProductIdNotFoundMessage(Guid? id) => $"Product with Id {id} has not been found";
        private static string VendorIdNotFoundMessage(Guid? id) => $"Vendor with Id {id} has not been found";
    }
}
