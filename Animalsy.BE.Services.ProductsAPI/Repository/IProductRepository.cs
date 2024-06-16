using Animalsy.BE.Services.ProductAPI.Models.Dto;

namespace Animalsy.BE.Services.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    Task<IEnumerable<ProductResponseDto>> GetByVendorAsync(Guid vendorId, string categoryAndSubcategory = null);
    Task<IEnumerable<Guid>> GetVendorIdsByProductCategoryAsync(string categoryAndSubcategory);
    Task<Guid> CreateAsync(CreateProductDto productDto);
    Task<ProductResponseDto> GetByIdAsync(Guid productId);
    Task<bool> TryUpdateAsync(UpdateProductDto productDto);
    Task<bool> TryDeleteAsync(Guid productId);
}