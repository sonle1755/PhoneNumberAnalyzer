namespace PhoneNumberAnalyzer.Business.Dtos;

public class UserDto
{
  public UserDto(int id,
                 string firstName,
                 string lastName,
                 string username,
                 string? avatarUrl,
                 bool emailVerified,
                 string? email,
                 DateTimeOffset? deletedAt,
                 DateTimeOffset? lastLoginAt)
  {
    Id = id;
    FirstName = firstName;
    LastName = lastName;
    Username = username;
    AvatarUrl = avatarUrl;
    EmailVerified = emailVerified;
    Email = email;
    DeletedAt = deletedAt;
    LastLoginAt = lastLoginAt;
  }

  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;

  public string LastName { get; set; } = string.Empty;

  public string Username { get; set; } = string.Empty;

  public string? AvatarUrl { get; set; } = null;

  public bool EmailVerified { get; set; } = false;

  public string? Email { get; set; } = null;

  public DateTimeOffset? DeletedAt { get; set; } = null;

  public DateTimeOffset? LastLoginAt { get; set; } = null;
}

