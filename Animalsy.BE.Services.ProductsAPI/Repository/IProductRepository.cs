using Animalsy.BE.Services.ProductsAPI.Models.Dto;

namespace Animalsy.BE.Services.ProductsAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetByVendorAsync(Guid vendorId);
        Task<Guid> CreateAsync(CreateProductDto productDto);
        Task<ProductDto?> GetByIdAsync(Guid productId);
        Task<bool> TryUpdateAsync(UpdateProductDto productDto);
        Task<bool> TryDeleteAsync(Guid productId);
    }
}
