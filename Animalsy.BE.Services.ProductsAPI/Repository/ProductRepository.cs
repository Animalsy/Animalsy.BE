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

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var results = await _dbContext.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(results);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetByVendorAsync(Guid vendorId, string categoryAndSubcategory = null)
    {
        var query = _dbContext.Products.Where(p => p.VendorId == vendorId);

        if (categoryAndSubcategory != null)
        {
            query = query.Where(p => p.CategoryAndSubCategory == categoryAndSubcategory);
        }

        var results = await query.ToListAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(results);
    }

    public async Task<IEnumerable<Guid>> GetVendorIdsByProductCategoryAsync(string categoryAndSubcategory)
    {
        return await _dbContext.Products
            .Where(p => p.CategoryAndSubCategory.StartsWith(categoryAndSubcategory))
            .Select(p => p.VendorId)
            .Distinct()
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<Guid> CreateAsync(CreateProductDto productDto)
    {
        var product = _mapper.Map<Product>(createProductDto);
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return product.Id;
    }

    public async Task<ProductDto> GetByIdAsync(Guid productId)
    {
        var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        return _mapper.Map<ProductDto>(result);
    }

    public async Task<bool> TryUpdateAsync(UpdateProductDto updateProductDto)
    {
        var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == updateProductDto.Id);
        if (existingProduct == null) return false;

        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.CategoryAndSubCategory = productDto.CategoryAndSubCategory;
        existingProduct.MinPrice = productDto.MinPrice;
        existingProduct.MaxPrice = productDto.MaxPrice;
        existingProduct.PromoPrice = productDto.PromoPrice;
        existingProduct.Duration = productDto.Duration;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task DeleteAsync(ProductDto productDto)
    {
        _dbContext.Products.Remove(_mapper.Map<Product>(productDto));
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}