namespace PhoneNumberAnalyzer.Business.Dtos;

public sealed record PhoneAnalysisResult(
    string OriginalInput,
    string NormalizedDigits,
    IReadOnlyCollection<PatternTemplateMatch> MatchedPatterns
// int TotalScore,
// string ValueTier // e.g. "Standard", "Good", "Premium", "VIP"
);
