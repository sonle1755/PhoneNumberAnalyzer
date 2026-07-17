using System.Collections.Immutable;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Helpers;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public class PatternAnalyzerService()
    : IPatternAnalyzerService
{

    private async Task<ImmutableArray<CompiledRegexPatterns>> GetCompiledRegexPatternsAsync()
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<MatchedPatternDto> FindMatchedPatterns(string pNumber, ImmutableArray<CompiledRegexPatterns> patterns)
    {
        foreach (var p in patterns)
        {
            var match = p.RegexPattern.Match(pNumber);
            if (match.Success) yield return new MatchedPatternDto(p.Name, match.Index, match.Length);
        }
    }

    public async Task<PatternAnalyzeResultDto> AnalyzeAsync(string input)
    {
        var compiledPatterns = await GetCompiledRegexPatternsAsync();
        var pNumber = VietnamPhoneHelpers.Normalize(input);
        if (pNumber is null) return new PatternAnalyzeResultDto(originalText: input,
                                                                extractedPhoneNumber: pNumber,
                                                                matchedPatterns: []);

        var matchedPatterns = FindMatchedPatterns(pNumber, compiledPatterns);
        return new PatternAnalyzeResultDto(originalText: input,
                                           extractedPhoneNumber: pNumber,
                                           matchedPatterns: [.. matchedPatterns]);
    }

    public async Task<ImmutableArray<PatternAnalyzeResultDto>> BulkAnalyzeAsync(ImmutableArray<string> input)
    {
        throw new NotImplementedException();
    }
}
