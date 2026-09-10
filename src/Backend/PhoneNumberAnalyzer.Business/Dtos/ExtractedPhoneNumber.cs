namespace PhoneNumberAnalyzer.Business.Dtos;

public sealed record ExtractedPhoneNumber(string RawMatchedText,
                                          string NormalizedDigits,
                                          int StartIndex);
