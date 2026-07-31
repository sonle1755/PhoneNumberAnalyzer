namespace PhoneNumberAnalyzer.Data.Entities;

/// <summary>
/// Join entity: one row per digit position (1-9) that a PatternDigitRule applies to.
/// </summary>
public sealed class PatternDigitRulePosition
{
    public Guid Id { get; set; }

    public Guid PatternDigitRuleId { get; set; }
    public PatternDigitRule PatternDigitRule { get; set; } = null!;

    /// <summary>
    /// Digit position within the phone number, 1-9 (position 0 is always the fixed leading '0').
    /// </summary>
    public int Position { get; set; }
}
