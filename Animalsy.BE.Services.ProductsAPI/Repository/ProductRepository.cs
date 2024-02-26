using Animalsy.BE.Services.ProductsAPI.Data;
using Animalsy.BE.Services.ProductsAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ProductsAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByVendorAsync(Guid vendorId)
        {
            return await _dbContext.Products
                .Where(p => p.VendorId == vendorId)
                .ToListAsync();
        }

        public async Task<Guid> CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<bool> TryUpdateAsync(Guid productId, Product product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (existingProduct == null) return false;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Category = product.Category;
            existingProduct.SubCategory = product.SubCategory;
            existingProduct.MinPrice = product.MinPrice;
            existingProduct.MaxPrice = product.MaxPrice;
            existingProduct.PromoPrice = product.PromoPrice;
            existingProduct.Duration = product.Duration;
            existingProduct.Services = product.Services;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid productId)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (existingProduct == null) return false;

            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
