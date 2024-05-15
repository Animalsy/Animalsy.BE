using Animalsy.BE.Services.PetsAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.PetsAPI.Utilities;

internal static partial class AppExtensions
{
    public static void ApplyPendingMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any()) dbContext.Database.Migrate();
    }
}