using Animalsy.BE.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ProductAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}