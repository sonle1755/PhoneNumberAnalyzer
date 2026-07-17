using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public class PatternService([NotNull] IPatternRepository patternRepository) : IPatternService
{
    private readonly IPatternRepository _patternRepository = patternRepository;

    public async Task<PatternDto> AddAsync(string name, string description, string regexString)
    {
        var pattern = await _patternRepository.AddAsync(name, description, regexString);
        return new PatternDto(pattern.Name, pattern.Description, pattern.RegexString);
    }

    public async Task DeleteAsync(int id)
    {
        await _patternRepository.DeleteAsync(id);
    }

    public async Task<ImmutableArray<PatternDto>> GetAllAsync()
    {
        var patterns = await _patternRepository.GetAllAsync();
        return [.. patterns.Select(p => new PatternDto(p.Name, p.Description, p.RegexString))];
    }

    public async Task<PatternDto?> GetPatternById(int id)
    {
        var pattern = await _patternRepository.GetByIdAsync(id);
        return pattern is null
            ? null
            : new PatternDto(pattern.Name, pattern.Description, pattern.RegexString);
    }
}
