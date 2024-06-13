using Animalsy.BE.Services.ProductAPI.Data;
using Animalsy.BE.Services.ProductAPI.Models;
using Animalsy.BE.Services.ProductAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var results = await _dbContext.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(results);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByVendorAsync(Guid vendorId)
    {
        var results = await _dbContext.Products
            .Where(p => p.VendorId == vendorId)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(results);
    }

    public async Task<Guid> CreateAsync(CreateProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product.Id;
    }

    public async Task<ProductResponseDto> GetByIdAsync(Guid productId)
    {
        var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        return _mapper.Map<ProductResponseDto>(result);
    }

    public async Task<bool> TryUpdateAsync(UpdateProductDto productDto)
    {
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
        if (existingProduct == null) return false;

        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.Category = productDto.Category;
        existingProduct.SubCategory = productDto.SubCategory;
        existingProduct.MinPrice = productDto.MinPrice;
        existingProduct.MaxPrice = productDto.MaxPrice;
        existingProduct.PromoPrice = productDto.PromoPrice;
        existingProduct.Duration = productDto.Duration;

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