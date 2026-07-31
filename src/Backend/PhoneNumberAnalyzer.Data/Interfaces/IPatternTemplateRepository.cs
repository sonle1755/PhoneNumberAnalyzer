using System.Collections.Immutable;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IPatternTemplateRepository
{
    Task<PatternTemplate> AddAsync(string name, string description, ImmutableArray<PatternDigitRule> rules, int? ownerId);

    Task UpdateAsync(Guid patternTemplateId, string name, string description, ImmutableArray<PatternDigitRule> rules, int? ownerId);

    Task DisableAsync(Guid patternTemplateId);

    Task EnableAsync(Guid patternTemplateId);
}
