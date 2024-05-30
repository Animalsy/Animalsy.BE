using Animalsy.BE.Services.PetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }
    }
}