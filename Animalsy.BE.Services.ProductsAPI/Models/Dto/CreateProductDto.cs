namespace Animalsy.BE.Services.ProductsAPI.Models.Dto
{
    public class CreateProductDto
    {
        public Guid VendorId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public decimal MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? PromoPrice { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
