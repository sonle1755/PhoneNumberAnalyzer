using System.Collections.Immutable;

namespace PhoneNumberAnalyzer.Data.Entities;

/// <summary>
/// A user-defined (or public/system) pattern made of up to 9 digit rules,
/// applied against positions 1-9 of a phone number (position 0, the leading
/// '0', is always fixed and never needs a rule).
/// </summary>
public sealed class PatternTemplate
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int? OwnerId { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset? DeletedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<PatternDigitRule> Rules { get; set; } = [];

    public static PatternTemplate Create(string name,
                                         string description,
                                         ImmutableArray<PatternDigitRule> rules,
                                         int? ownerId)
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name), "PatternTempalte Name cannot be null!");

        if (description == null)
        {
            throw new ArgumentNullException(nameof(description),
                                            "PatternTemplate Description cannot be null!");
        }

        return new PatternTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Rules = rules,
            OwnerId = ownerId,
            IsEnabled = true,
            CreatedAt = DateTimeOffset.UtcNow,
            DeletedAt = null,
            UpdatedAt = null
        };
    }
}
