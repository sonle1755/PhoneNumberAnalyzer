using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Data.Entities;

/// <summary>
/// A single rule belonging to a PatternTemplate. One rule can apply to multiple
/// digit positions (see PatternDigitRulePosition), e.g. "digits 3, 6, 9 must equal 8".
/// </summary>
public sealed class PatternRule
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid GroupId { get; set; }

    public PatternRuleGroup Group { get; set; } = null!;

    public PatternRuleType RuleType { get; set; }

    public int Length { get; set; }

    // Must be from 1 - 9
    public int[] TargetPositions { get; set; } = [];

    public int? ReferencePosition { get; set; }

    public string[] Values { get; set; } = [];
}
