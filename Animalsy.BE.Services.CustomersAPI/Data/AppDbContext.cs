using Animalsy.BE.Services.CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomerAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
}