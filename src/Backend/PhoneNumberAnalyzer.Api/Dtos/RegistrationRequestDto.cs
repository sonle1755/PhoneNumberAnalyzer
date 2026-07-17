namespace PhoneNumberAnalyzer.Api.Dtos;

public record RegistrationRequestDto(string FirstName,
                                     string LastName,
                                     string UserName,
                                     string Password,
                                     string? AvatarUrl,
                                     string? Email);
