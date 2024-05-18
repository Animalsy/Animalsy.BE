using Animalsy.BE.Services.VendorsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VendorsAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vendor> Vendors { get; set; }
}