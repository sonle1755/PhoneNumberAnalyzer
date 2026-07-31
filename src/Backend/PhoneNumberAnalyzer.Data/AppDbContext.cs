using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserAuthProvider> UserAuthProviders => Set<UserAuthProvider>();
    public DbSet<PatternTemplate> PatternTemplates => Set<PatternTemplate>();
    public DbSet<PatternDigitRule> PatternDigitRules => Set<PatternDigitRule>();
    public DbSet<PatternDigitRulePosition> PatternDigitRulePositions => Set<PatternDigitRulePosition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.AuthProviders)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<PatternTemplate>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(500);

            // OwnerId is nullable = public template. Cascade delete when the owning
            // user is deleted, per "all their private templates get deleted too".
            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasIndex(t => t.OwnerId);
        });

        modelBuilder.Entity<PatternDigitRule>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.PatternTemplate)
                .WithMany(t => t.Rules)
                .HasForeignKey(r => r.PatternTemplateId)
                .OnDelete(DeleteBehavior.Cascade); // deleting a template removes its rules

            entity.Property(r => r.DigitValue).HasAnnotation("Range", new[] { 0, 9 });
            entity.Property(r => r.ReferencePosition).HasAnnotation("Range", new[] { 1, 9 });
        });

        modelBuilder.Entity<PatternDigitRulePosition>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.PatternDigitRule)
                .WithMany(r => r.Positions)
                .HasForeignKey(p => p.PatternDigitRuleId)
                .OnDelete(DeleteBehavior.Cascade); // deleting a rule removes its positions

            entity.Property(p => p.Position).IsRequired();

            // A single rule shouldn't target the same position twice.
            entity.HasIndex(p => new { p.PatternDigitRuleId, p.Position }).IsUnique();
        });

    }
}
