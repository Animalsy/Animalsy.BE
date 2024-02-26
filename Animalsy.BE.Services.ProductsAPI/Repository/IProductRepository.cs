using Animalsy.BE.Services.ProductsAPI.Models;

namespace Animalsy.BE.Services.ProductsAPI.Repository
{
    public interface IProductRepository
    {
        Task<Guid> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetByVendorAsync(Guid vendorId);
        Task<Product?> GetByIdAsync(Guid productId);
        Task<bool> TryUpdateAsync(Guid productId, Product product);
        Task<bool> TryDeleteAsync(Guid productId);
    }
}
