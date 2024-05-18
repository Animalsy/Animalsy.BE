using Animalsy.BE.Services.CustomersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.CustomersAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
}