using Animalsy.BE.Services.PetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
    }
}
