using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HackChallengeApi.Models;

namespace HackChallengeApi.Models;

public sealed class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }

public DbSet<HackChallengeApi.Models.TestClass> TestClass { get; set; } = default!;
}