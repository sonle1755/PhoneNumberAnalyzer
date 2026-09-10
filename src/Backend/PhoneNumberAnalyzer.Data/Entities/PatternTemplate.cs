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

    public User? Owner { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<PatternRuleGroup> RuleGroups { get; set; } = [];

}
