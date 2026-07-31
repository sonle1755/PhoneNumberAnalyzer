namespace PhoneNumberAnalyzer.Business.Dtos;

public sealed record PatternResult(
    string Key,
    string DisplayName,
    bool Matched,
    string? MatchedSegment,
    int Weight,
    string? Description = null
);
