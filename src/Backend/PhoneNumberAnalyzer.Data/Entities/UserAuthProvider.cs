namespace PhoneNumberAnalyzer.Data.Entities;

public class UserAuthProvider {
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Provider { get; set; } = string.Empty;

    public string? ProviderUserId { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public User User { get; set; } = null!;

    public static UserAuthProvider Create(int userId, string provider, string passwordHash, string? providerUserId) {
        return new UserAuthProvider {
            Id = 0,
            UserId = userId,
            Provider = provider,
            PasswordHash = passwordHash,
            ProviderUserId = providerUserId,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
}

