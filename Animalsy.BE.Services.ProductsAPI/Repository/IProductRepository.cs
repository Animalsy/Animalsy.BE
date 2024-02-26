using Animalsy.BE.Services.ProductsAPI.Models;

namespace Animalsy.BE.Services.ProductsAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByVendorAsync(Guid vendorId);
        Task<Guid> CreateAsync(Product product);
        Task<Product?> GetByIdAsync(Guid productId);
        Task<bool> TryUpdateAsync(Guid productId, Product product);
        Task<bool> TryDeleteAsync(Guid productId);
    }
}
