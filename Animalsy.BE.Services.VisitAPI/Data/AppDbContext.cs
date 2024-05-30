using Animalsy.BE.Services.VisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VisitAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Visit> Visits { get; set; }
}