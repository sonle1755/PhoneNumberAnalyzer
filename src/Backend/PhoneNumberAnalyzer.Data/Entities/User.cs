namespace PhoneNumberAnalyzer.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; }

    public bool EmailVerified { get; set; }

    public string? Email { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public DateTimeOffset? LastLoginAt { get; set; }

    public ICollection<UserAuthProvider> AuthProviders { get; set; } = [];

    public ICollection<PatternTemplate> PatternTemplates { get; set; } = [];
}

