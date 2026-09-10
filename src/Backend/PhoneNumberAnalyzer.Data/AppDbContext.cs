using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserAuthProvider> UserAuthProviders => Set<UserAuthProvider>();
    public DbSet<PatternTemplate> PatternTemplates => Set<PatternTemplate>();
    public DbSet<PatternRuleGroup> PatternRuleGroups => Set<PatternRuleGroup>();
    public DbSet<PatternRule> PatternRules => Set<PatternRule>();

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
            entity.HasOne(e => e.Owner)
                .WithMany(e => e.PatternTemplates)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            entity.HasIndex(t => t.OwnerId);

            entity
                .HasMany(e => e.RuleGroups)
                .WithOne(e => e.PatternTemplate)
                .HasForeignKey(e => e.PatternTemplateId)
                .IsRequired();
        });

        modelBuilder.Entity<PatternRuleGroup>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            // Max group nesting level will be defined in business layer
            entity.Property(t => t.Level).IsRequired();
            entity.Property(t => t.RuleOperator).IsRequired().HasDefaultValue(RuleOperator.And);

            entity.HasOne(r => r.PatternTemplate)
                  .WithMany(t => t.RuleGroups)
                  .HasForeignKey(r => r.PatternTemplateId)
                  .OnDelete(DeleteBehavior.Cascade) // deleting a template removes its rule groups
                  .IsRequired();

            entity.HasOne(r => r.ParentGroup)
                  .WithMany(t => t.ChildGroups)
                  .HasForeignKey(r => r.ParentId)
                  .OnDelete(DeleteBehavior.Cascade) // deleting a group removes its child groups
                  .IsRequired(false);

        });

        modelBuilder.Entity<PatternRule>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            // A phone number only consist of 10 number (except for the fisrt number - 0)
            entity.Property(t => t.Length).IsRequired().HasMaxLength(9);

            entity.HasOne(p => p.Group)
                .WithMany(r => r.Rules)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Cascade) // deleting a group removes its rules
                .IsRequired();
        });

    }
}
