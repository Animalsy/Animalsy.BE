using Animalsy.BE.Services.ContractorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.ContractorAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contractor> Contractors { get; set; }
}