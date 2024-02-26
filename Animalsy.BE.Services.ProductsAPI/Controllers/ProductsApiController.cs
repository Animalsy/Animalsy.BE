using Animalsy.BE.Services.ProductsAPI.Models;
using Animalsy.BE.Services.ProductsAPI.Models.Dto;
using Animalsy.BE.Services.ProductsAPI.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.ProductsAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsApiController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet("GetProducts")]
        public async Task<ResponseDto> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Any()
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = products,
                }
                : new ResponseDto
                {
                    Message = "There were no products added yet"
                };
        }

        [HttpGet("GetProducts/{vendorId}")]
        public async Task<ResponseDto> GetByVendorAsync([FromRoute] Guid vendorId)
        {
            var products = await _productRepository.GetByVendorAsync(vendorId);
            return products.Any()
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = products,
                }
                : new ResponseDto
                {
                    Message = "There were no products added yet"
                };
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<ResponseDto> GetByIdAsync([FromRoute] Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product != null
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = product,
                }
                : new ResponseDto
                {
                    Message = "Product with provided Id has not been found"
                };
        }

        [HttpPost("CreateProduct")]
        public async Task<ResponseDto> CreateAsync([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var createdProductId = await _productRepository.CreateAsync(_mapper.Map<Product>(dto));
            return new ResponseDto
            {
                IsSuccess = true,
                Result = createdProductId,
                Message = "Product has been created successfully"
            };
        }

        [HttpPost("UpdateProduct/{productId}")]
        public async Task<ResponseDto> UpdateAsync([FromRoute] Guid productId, [FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return new ResponseDto
            {
                Result = ModelState
            };

            var updateResult = await _productRepository.TryUpdateAsync(productId, _mapper.Map<Product>(dto));
            return new ResponseDto
            {
                IsSuccess = updateResult,
                Result = productId,
                Message = updateResult ? "Product has been updated successfully" : "Product with provided Id has not been found"
            };
        }

        [HttpGet("DeleteProduct/{productId}")]
        public async Task<ResponseDto> DeleteAsync([FromRoute] Guid productId)
        {
            var deleteResult = await _productRepository.TryDeleteAsync(productId);
            return new ResponseDto
            {
                IsSuccess = deleteResult,
                Result = productId,
                Message = deleteResult ? "Product has been deleted successfully" : "Product with provided Id has not been found"
            };
        }
    }
}
