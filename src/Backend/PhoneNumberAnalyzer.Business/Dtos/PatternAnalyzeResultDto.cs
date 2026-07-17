using System.Collections.Immutable;

namespace PhoneNumberAnalyzer.Business.Dtos;

public class PatternAnalyzeResultDto(string originalText,
                                     string? extractedPhoneNumber,
                                     ImmutableArray<MatchedPatternDto> matchedPatterns)
{
    public string OriginalText { get; set; } = originalText ?? throw new ArgumentNullException(nameof(originalText));

    public string? ExtractedPhoneNumber { get; set; } = extractedPhoneNumber;

    public ImmutableArray<string> ErrorMessages { get; set; } = extractedPhoneNumber != null ? [] : ["The original phone number is not in a valid Vietnam phone number format!"];

    public ImmutableArray<MatchedPatternDto> MatchedPatterns { get; set; } = matchedPatterns;
}

public class MatchedPatternDto(string patternName, int startAt, int length)
{
    public string PatternName { get; set; } = patternName ?? throw new ArgumentNullException(nameof(patternName));

    public int StartAt { get; set; } = startAt;

    public int Length { get; set; } = length;
}
