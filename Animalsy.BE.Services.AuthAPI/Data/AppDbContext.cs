using Animalsy.BE.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.AuthAPI.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

        builder.Entity<ApplicationRole>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
        });

        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
            new ApplicationRole { Id = Guid.NewGuid(), Name = "Customer", NormalizedName = "CUSTOMER" },
            new ApplicationRole { Id = Guid.NewGuid(), Name = "Vendor", NormalizedName = "VENDOR" }
        );
    }
}