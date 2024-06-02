using Microsoft.AspNetCore.Identity;

namespace Animalsy.BE.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CustomerId { get; set; }
        public Guid VendorId { get; set; }
    }
}
