namespace PhoneNumberAnalyzer.Api.Dtos;

public sealed record LoginResponse(string AccessToken, int ExpiresIn);
