using Animalsy.BE.Services.ProductAPI.Models.Dto;

namespace Animalsy.BE.Services.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<IEnumerable<ProductDto>> GetByVendorAsync(Guid vendorId);
    Task<Guid> CreateAsync(CreateProductDto createProductDto);
    Task<ProductDto> GetByIdAsync(Guid productId);
    Task<bool> TryUpdateAsync(UpdateProductDto updateProductDto);
    Task DeleteAsync(ProductDto productDto);
}