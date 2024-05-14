using Animalsy.BE.Services.PetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetsAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }
    }
}
