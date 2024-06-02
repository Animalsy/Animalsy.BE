
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.AuthAPI.Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{
}