namespace PhoneNumberAnalyzer.Data.Entities;

public class User
{
    public User() { }

    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; } = null;

    public bool EmailVerified { get; set; } = false;

    public string? Email { get; set; } = null;

    public DateTimeOffset? DeletedAt { get; set; } = null;

    public DateTimeOffset? LastLoginAt { get; set; } = null;

    public ICollection<UserAuthProvider> AuthProviders { get; set; } = [];

    public ICollection<PatternTemplate> PatternTemplates { get; set; } = [];

    public static User Create(string firstName,
                              string lastName,
                              string username,
                              string? avatarUrl,
                              string? email)
    {
        return new User
        {
            Id = 0,
            FirstName = firstName,
            LastName = lastName,
            Username = username,
            AvatarUrl = avatarUrl,
            EmailVerified = false,
            Email = email,
            DeletedAt = null,
            LastLoginAt = null
        };
    }
}

