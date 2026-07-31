using System.Collections.Immutable;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Data.Entities;

/// <summary>
/// A single rule belonging to a PatternTemplate. One rule can apply to multiple
/// digit positions (see PatternDigitRulePosition), e.g. "digits 3, 6, 9 must equal 8".
/// </summary>
public sealed class PatternDigitRule
{
    public Guid Id { get; set; }

    public Guid PatternTemplateId { get; set; }

    public PatternTemplate PatternTemplate { get; set; } = null!;

    public DigitRuleType RuleType { get; set; }

    /// <summary>
    /// Used only when RuleType is FixedDigit - the required digit value (0-9).
    /// </summary>
    public int? DigitValue { get; set; }

    /// <summary>
    /// Used only when RuleType is EqualsPosition/NotEqualsPosition/GreaterThanPosition/
    /// LessThanPosition - the other position (1-9) this rule compares against.
    /// </summary>
    public int? ReferencePosition { get; set; }

    public ICollection<PatternDigitRulePosition> Positions { get; set; } = [];

    public static PatternDigitRule Create(Guid patternTemplateId,
                                          DigitRuleType ruleType,
                                          int? digitValue,
                                          int? referencePosition,
                                          ImmutableArray<PatternDigitRulePosition> positions)
    {

        if (digitValue != null)
        {
            if (digitValue < 0 || digitValue > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digitValue),
                                                      "Digit value must be in between 0 and 9!");
            }
            if (ruleType != DigitRuleType.FixedDigit)
            {
                throw new InvalidOperationException("Digit value can only be set when rule type is fixed digit!");
            }
        }

        if (ruleType == DigitRuleType.SameAs && positions.Length < 2)
        {
            throw new InvalidOperationException("Rule type SameAs can only be apply when there're multiple positions!");
        }

        var rulesRequireReference = new DigitRuleType[]
        {
            DigitRuleType.EqualsPosition,
            DigitRuleType.NotEqualsPosition,
            DigitRuleType.LessThanPosition,
            DigitRuleType.GreaterThanPosition
        };

        if (rulesRequireReference.Contains(ruleType) && referencePosition == null)
        {
            throw new InvalidOperationException("ReferencePosition is required!");
        }

        return new PatternDigitRule
        {
            Id = Guid.NewGuid(),
            PatternTemplateId = patternTemplateId,
            RuleType = ruleType,
            DigitValue = digitValue,
            ReferencePosition = referencePosition,
            Positions = positions
        };
    }
}
