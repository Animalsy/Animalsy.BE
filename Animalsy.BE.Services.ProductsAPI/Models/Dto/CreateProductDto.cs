namespace Animalsy.BE.Services.ProductsAPI.Models.Dto
{
    public class CreateProductDto
    {
        public Guid VendorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public TimeSpan Duration { get; set; }
        public IEnumerable<string> Services { get; set; }
    }
}
