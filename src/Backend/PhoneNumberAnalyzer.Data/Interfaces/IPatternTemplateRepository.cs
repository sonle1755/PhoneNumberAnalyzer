using System.Collections.Immutable;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IPatternTemplateRepository
{
    Task<ImmutableArray<PatternTemplate>> GetVisibleToUserAsync(int userId);

    Task<ImmutableArray<PatternTemplate>> GetPublicAsync();

    Task<PatternTemplate?> GetByIdAsync(Guid patternTemplateId);

    Task<PatternTemplate> AddAsync(string name, string description);

    Task UpdateAsync(Guid patternTemplateId, string name, string description);

    Task DisableAsync(Guid patternTemplateId);

    Task EnableAsync(Guid patternTemplateId);

    Task SaveChangesAsync();
}
