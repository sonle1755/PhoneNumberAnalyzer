using System.Collections.Immutable;
using PhoneNumberAnalyzer.Business.Dtos;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPatternAnalyzerService
{
    public Task<PatternAnalyzeResultDto> AnalyzeAsync(string input);

    public Task<ImmutableArray<PatternAnalyzeResultDto>> BulkAnalyzeAsync(ImmutableArray<string> input);
}
