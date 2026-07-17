using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<UserAuthProvider> UserAuthProviders => Set<UserAuthProvider>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.AuthProviders)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}
