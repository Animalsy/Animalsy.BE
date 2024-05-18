using Animalsy.BE.Services.ProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ProductsAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    {
    }

    public DbSet<Product> Products { get; set; }
}