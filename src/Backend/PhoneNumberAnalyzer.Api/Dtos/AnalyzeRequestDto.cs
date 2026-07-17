using System.Collections.Immutable;

namespace PhoneNumberAnalyzer.Api.Dtos;

public record AnalyzeRequestDto(ImmutableArray<string> PhoneNumbers);
