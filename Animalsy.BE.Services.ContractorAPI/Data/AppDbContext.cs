using Animalsy.BE.Services.ContractorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ContractorAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contractor> Contractors { get; set; }
}