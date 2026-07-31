using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class PatternTemplateRepository(AppDbContext db) : IPatternTemplateRepository
{
    private readonly AppDbContext _db = db;

    private async Task<PatternTemplate?> GetByIdAsync(Guid patternTemplateId)
    {
        return await _db.PatternTemplates.AsNoTracking()
                                         .Where(pt => pt.Id == patternTemplateId)
                                         .FirstOrDefaultAsync();
    }

    public async Task<PatternTemplate> AddAsync(string name,
                                                string description,
                                                ImmutableArray<PatternDigitRule> rules,
                                                int? ownerId)
    {
        var user = PatternTemplate.Create(name, description, rules, ownerId);
        await _db.PatternTemplates.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(Guid patternTemplateId,
                                  string name,
                                  string description,
                                  ImmutableArray<PatternDigitRule> rules,
                                  int? ownerId)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.Name = name;
        patternTemplate.Description = description;
        patternTemplate.Rules = rules;
        patternTemplate.OwnerId = ownerId;
        await _db.SaveChangesAsync();
    }

    public async Task DisableAsync(Guid patternTemplateId)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.IsEnabled = false;
        await _db.SaveChangesAsync();
    }

    public async Task EnableAsync(Guid patternTemplateId)
    {
        var patternTemplate = await GetByIdAsync(patternTemplateId)
            ?? throw new InvalidOperationException($"Found no PatternTemplate with matching Id {patternTemplateId}");
        patternTemplate.IsEnabled = true;
        await _db.SaveChangesAsync();
    }
}
