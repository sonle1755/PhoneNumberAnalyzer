namespace PhoneNumberAnalyzer.Data.Enums;

/// <summary>
/// Defines how a PatternRule evaluates the digit(s) at its target position(s).
/// Position semantics (which positions are targets vs references, or whether
/// the rule applies to the whole number) come from PatternRulePosition, not this enum.
/// </summary>
public enum PatternRuleType
{
    // --- Relational checks against reference position(s) ---
    EqualsPosition = 1,          // digit at target must equal digit at reference position

    NotEqualsPosition = 2,       // digit at target must differ from digit at reference position

    GreaterThanPosition = 3,     // digit at target must be greater than digit at reference position

    LessThanPosition = 4,        // digit at target must be less than digit at reference position

    // --- Sequential checks (no reference position needed - compares to immediate neighbor) ---
    GreaterThanPrevious = 5,     // digit must be greater than the immediately preceding digit

    LessThanPrevious = 6,        // digit must be less than the immediately preceding digit

    // --- Value-set checks over a span (uses PatternRuleValue) ---
    ValueWhitelist = 7,          // digits at target position(s), read as one value, must be in the value set

    ValueBlacklist = 8,          // digits at target position(s), read as one value, must NOT be in the value set

    // --- Structural checks (no target/reference positions needed) ---
    ContainsRepeatedSubstring = 9 // some N-digit substring appears more than once anywhere in the number
}
