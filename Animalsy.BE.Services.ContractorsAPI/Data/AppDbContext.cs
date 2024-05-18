using Animalsy.BE.Services.ContractorsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ContractorsAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contractor> Contractors { get; set; }
}