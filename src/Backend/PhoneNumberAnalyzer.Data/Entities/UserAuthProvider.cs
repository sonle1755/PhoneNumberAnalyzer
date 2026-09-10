namespace PhoneNumberAnalyzer.Data.Entities;

public class UserAuthProvider
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public string Provider { get; set; } = string.Empty;

    public string? ProviderUserId { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

