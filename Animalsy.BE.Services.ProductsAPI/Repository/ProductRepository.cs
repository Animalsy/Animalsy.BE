using Animalsy.BE.Services.ProductsAPI.Data;
using Animalsy.BE.Services.ProductsAPI.Models;
using Animalsy.BE.Services.ProductsAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ProductsAPI.Repository
{
    public class ProductRepository(AppDbContext dbContext, IMapper mapper) : IProductRepository
    {
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var results = await dbContext.Products.ToListAsync();
            return mapper.Map<IEnumerable<ProductResponseDto>>(results);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetByVendorAsync(Guid vendorId)
        {
            var results = await dbContext.Products
                .Where(p => p.VendorId == vendorId)
                .ToListAsync();
            return mapper.Map<IEnumerable<ProductResponseDto>>(results);
        }

        public async Task<Guid> CreateAsync(CreateProductDto productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<ProductResponseDto?> GetByIdAsync(Guid productId)
        {
            var result = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return mapper.Map<ProductResponseDto>(result);
        }

        public async Task<bool> TryUpdateAsync(UpdateProductDto productDto)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
            if (existingProduct == null) return false;

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Category = productDto.Category;
            existingProduct.SubCategory = productDto.SubCategory;
            existingProduct.MinPrice = productDto.MinPrice;
            existingProduct.MaxPrice = productDto.MaxPrice;
            existingProduct.PromoPrice = productDto.PromoPrice;
            existingProduct.Duration = productDto.Duration;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid productId)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (existingProduct == null) return false;

            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
