namespace PhoneNumberAnalyzer.Api.Dtos;

public record RegisterRequest(string FirstName,
                              string LastName,
                              string Username,
                              string Password,
                              string? AvatarUrl,
                              string? Email);
