using Animalsy.BE.Services.ProductAPI.Models.Dto;
using Animalsy.BE.Services.ProductAPI.Repository;
using Animalsy.BE.Services.ProductAPI.Utilities;
using Animalsy.BE.Services.ProductAPI.Validators.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Animalsy.BE.Services.ProductAPI.Validators;
using FluentValidation.Results;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Animalsy.BE.Services.ProductAPI.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IValidatorFactory _validatorFactory;

    public ProductController(IProductRepository productRepository, IValidatorFactory validatorFactory)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
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

    [HttpGet("Vendor/{vendorId:guid}/{categoryAndSubCategory}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByVendorAsync([FromRoute] Guid vendorId, [FromRoute] string categoryAndSubCategory = null)
    {
        var validationResult = await ValidateVendorCategoryAsync(vendorId, categoryAndSubCategory);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var products = await _productRepository.GetByVendorAsync(vendorId, categoryAndSubCategory);
        return products.Any()
            ? Ok(products)
            : NotFound(VendorIdNotFoundMessage(vendorId, categoryAndSubCategory));
    }

    [HttpGet("Vendor/{categoryAndSubCategory}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVendorIdsByProductsCategoryAsync([FromRoute] string categoryAndSubCategory)
    {
        var validationResult = await new CategoryValidator(false).ValidateAsync(categoryAndSubCategory);
        if (!validationResult.IsValid) return BadRequest(validationResult);

        var vendorIds = await _productRepository.GetVendorIdsByProductCategoryAsync(categoryAndSubCategory);
        return vendorIds.Any()
            ? Ok(vendorIds)
            : NotFound(VendorIdNotFoundMessage(categoryAndSubCategory: categoryAndSubCategory));
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid productId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(productId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var product = await _productRepository.GetByIdAsync(productId);
        return product != null
            ? Ok(product)
            : NotFound(ProductIdNotFoundMessage(productId));
    }

    [HttpPost]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto productDto)
    {
        var validationResult = await _validatorFactory.GetValidator<CreateProductDto>()
            .ValidateAsync(productDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var createdProductId = await _productRepository.CreateAsync(productDto);
        return new ObjectResult(createdProductId) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductDto productDto)
    {
        var validationResult = await _validatorFactory.GetValidator<UpdateProductDto>()
            .ValidateAsync(productDto);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), productDto.UserId) || !User.IsInRole(SD.RoleAdmin))
            return Unauthorized();

        var updateResult = await _productRepository.TryUpdateAsync(productDto);
        return updateResult
            ? Ok("Product has been updated successfully")
            : NotFound(ProductIdNotFoundMessage(productDto.Id));
    }

    [HttpDelete("{productId:guid}")]
    [Authorize(Roles = SD.RoleAdminAndVendor)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid productId)
    {
        var validationResult = await _validatorFactory.GetValidator<Guid>()
            .ValidateAsync(productId);

        if (!validationResult.IsValid) return BadRequest(validationResult);

        var productDto = await _productRepository.GetByIdAsync(productId);
        if (productDto == null) return NotFound(ProductIdNotFoundMessage(productId));

        if (!CheckLoggedUser(User.FindFirst(JwtRegisteredClaimNames.Sub), productDto.UserId) || !User.IsInRole(SD.RoleAdmin))
            return Unauthorized();

        await _productRepository.DeleteAsync(productDto);
        return Ok("Product has been deleted successfully");
    }

    private static bool CheckLoggedUser(Claim claim, Guid requestedId)
    {
        return claim != null && Guid.TryParse(claim.Value, out var id) && id == requestedId;
    }

    private async Task<ValidationResult> ValidateVendorCategoryAsync(Guid vendorId, string categoryAndSubCategory)
    {
        if (categoryAndSubCategory == null)
        {
            return await _validatorFactory.GetValidator<Guid>().ValidateAsync(vendorId);
        }

        return await _validatorFactory.GetValidator<VendorCategoryDto>()
            .ValidateAsync(new VendorCategoryDto(vendorId, categoryAndSubCategory));
    }

    private static string ProductIdNotFoundMessage(Guid? id) => $"Product with Id {id} has not been found";

    private static string VendorIdNotFoundMessage(Guid? id = null, string categoryAndSubCategory = null)
    {
        return id switch
        {
            null when categoryAndSubCategory == null => "Vendor Id and category are required to retrieve products",
            null => $"Vendors with products for category {categoryAndSubCategory} have not been found",
            _ => categoryAndSubCategory == null
                ? $"Products for Vendor Id {id} have not been found"
                : $"Products for Vendor Id {id} and category {categoryAndSubCategory} have not been found"
        };
    }

}