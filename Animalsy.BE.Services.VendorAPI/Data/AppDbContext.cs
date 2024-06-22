using Animalsy.BE.Services.VendorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VendorAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Vendor> Vendors { get; set; }
}