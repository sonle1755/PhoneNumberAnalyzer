using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class PatternTemplateRepository(AppDbContext db) : IPatternTemplateRepository
{
    private readonly AppDbContext _db = db;

    public async Task<PatternTemplate?> GetByIdAsync(Guid patternTemplateId)
    {
        return await _db.PatternTemplates
            .Where(pt => pt.Id == patternTemplateId)
            .Include(pt => pt.RuleGroups)
                .ThenInclude(g => g.Rules)
            .FirstOrDefaultAsync();
    }

    public async Task<PatternTemplate> AddAsync(string name,
                                                string description)
    {
        var patternTemplate = new PatternTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            RuleGroups = [],
            Owner = null,
            CreatedAt = DateTimeOffset.UtcNow,
            DeletedAt = null,
            UpdatedAt = null
        };
        await _db.PatternTemplates.AddAsync(patternTemplate);
        return patternTemplate;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid patternTemplateId, string name, string description)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
                              ?? throw new ArgumentNullException(nameof(patternTemplateId));
        patternTemplate.Name = name;
        patternTemplate.Description = description;
    }

    public async Task<ImmutableArray<PatternTemplate>> GetVisibleToUserAsync(int userId)
    {
        var templates = await _db.PatternTemplates
            .Where(t => t.DeletedAt == null && (t.OwnerId == userId || t.OwnerId == null))
                .Include(t => t.RuleGroups)
                    .ThenInclude(g => g.Rules)
            .ToListAsync();

        return [.. templates];
    }

    public async Task<ImmutableArray<PatternTemplate>> GetPublicAsync()
    {
        var templates = await _db.PatternTemplates
            .Where(t => t.DeletedAt == null && t.OwnerId == null)
                .Include(t => t.RuleGroups)
                    .ThenInclude(g => g.Rules)
            .ToListAsync();

        return [.. templates];
    }

    public async Task UpdateAsync(Guid patternTemplateId,
                                  string name,
                                  string description,
                                  ImmutableArray<PatternRuleGroup> ruleGroups)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.Name = name;
        patternTemplate.Description = description;
        patternTemplate.RuleGroups = ruleGroups;
        patternTemplate.UpdatedAt = DateTimeOffset.UtcNow;
    }

    public async Task DisableAsync(Guid patternTemplateId)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.DeletedAt = DateTimeOffset.UtcNow;
    }

    public async Task EnableAsync(Guid patternTemplateId)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.DeletedAt = null;
    }
}
