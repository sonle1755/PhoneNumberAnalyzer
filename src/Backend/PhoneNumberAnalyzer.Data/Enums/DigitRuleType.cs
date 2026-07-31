namespace PhoneNumberAnalyzer.Data.Enums;

/// <summary>
/// Defines how a PatternDigitRule evaluates the digit(s) at its assigned positions.
/// A position with no PatternDigitRule row is unconstrained (matches any digit) -
/// there is no explicit "Any" rule type, since that would be a redundant way to
/// express "no rule."
/// </summary>
public enum DigitRuleType
{
    // digit must equal a specific value (uses DigitValue)
    FixedDigit = 1,

    // digit must equal the digit at ReferencePosition
    EqualsPosition = 2,

    // digit must differ from the digit at ReferencePosition
    NotEqualsPosition = 3,

    // digit must be greater than the digit at ReferencePosition
    GreaterThanPosition = 4,

    // digit must be less than the digit at ReferencePosition
    LessThanPosition = 5,

    // digit must be greater than the immediately preceding digit
    GreaterThanPrevious = 6,

    // digit must be less than the immediately preceding digit
    LessThanPrevious = 7,

    // digits at Positions must have the same value, this enum value only available when select
    // multiple Positions
    SameAs = 8
}
