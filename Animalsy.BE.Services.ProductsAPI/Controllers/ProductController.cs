using Animalsy.BE.Services.ProductAPI.Models.Dto;
using Animalsy.BE.Services.ProductAPI.Repository;
using Animalsy.BE.Services.ProductAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.ProductAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly UniqueIdValidator _uniqueIdValidator;
    private readonly CreateProductValidator _createProductValidator;
    private readonly UpdateProductValidator _updateProductValidator;

    public ProductController(IProductRepository productRepository, UniqueIdValidator uniqueIdValidator, 
        CreateProductValidator createProductValidator, UpdateProductValidator updateProductValidator)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _uniqueIdValidator = uniqueIdValidator ?? throw new ArgumentNullException(nameof(uniqueIdValidator));
        _createProductValidator = createProductValidator ?? throw new ArgumentNullException(nameof(createProductValidator));
        _updateProductValidator = updateProductValidator ?? throw new ArgumentNullException(nameof(updateProductValidator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Any()
            ? Ok(products)
            : NotFound("There were no products added yet");
    }

    [HttpGet("Vendor/{vendorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByVendorAsync([FromRoute] Guid vendorId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(vendorId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var products = await _productRepository.GetByVendorAsync(vendorId);
        return products.Any()
            ? Ok(products)
            : NotFound(VendorIdNotFoundMessage(vendorId));
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid productId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(productId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var product = await _productRepository.GetByIdAsync(productId);
        return product != null
            ? Ok(product)
            : NotFound(ProductIdNotFoundMessage(productId));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto productDto)
    {
        var validationResult = await _createProductValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdProductId = await _productRepository.CreateAsync(productDto);
        return Ok(createdProductId);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductDto productDto)
    {
        var validationResult = await _updateProductValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var updateResult = await _productRepository.TryUpdateAsync(productDto);
        return updateResult
            ? Ok("Product has been updated successfully")
            : NotFound(ProductIdNotFoundMessage(productDto.Id));
    }

    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid productId)
    {
        var validationResult = await _uniqueIdValidator.ValidateAsync(productId);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var deleteResult = await _productRepository.TryDeleteAsync(productId);
        return deleteResult
            ? Ok("Product has been deleted successfully")
            : NotFound(ProductIdNotFoundMessage(productId));
    }

    private static string ProductIdNotFoundMessage(Guid? id) => $"Product with Id {id} has not been found";
    private static string VendorIdNotFoundMessage(Guid? id) => $"Vendor with Id {id} has not been found";
}