using System.Collections.Immutable;
using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPatternService
{
    public Task<ImmutableArray<PatternDto>> GetAllAsync();

    public Task<PatternDto?> GetPatternById(int id);

    public Task<PatternDto> AddAsync(string name, string description, string regexString);

    public Task DeleteAsync(int id);
}
