using PhoneNumberAnalyzer.Business.Specifications.Composite;
using PhoneNumberAnalyzer.Business.Specifications.Leaf;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Business.Specifications;

/// <summary>
/// Recursively converts a persisted PatternRuleGroup tree into a single
/// ISpecification. This is the only place that understands the entity
/// shape (PatternRuleGroup / PatternRule / PatternRulePosition / PatternRuleValue) -
/// everything under Specifications/ stays decoupled from Data entities.
/// </summary>
public static class PatternRuleGroupSpecificationBuilder
{
    public static ISpecification Build(PatternRuleGroup group)
    {
        // Per the "rules-only OR child-groups-only" invariant, exactly one
        // of these collections should be populated.
        if (group.Rules.Count > 0)
        {
            var ruleSpecs = group.Rules.Select(BuildRuleSpecification).ToList();
            return Combine(ruleSpecs, group.RuleOperator);
        }

        if (group.ChildGroups.Count > 0)
        {
            var childSpecs = group.ChildGroups.Select(Build).ToList(); // recursive call
            return Combine(childSpecs, group.RuleOperator);
        }

        throw new InvalidOperationException(
            $"PatternRuleGroup '{group.Id}' has neither rules nor child groups.");
    }

    private static ISpecification Combine(IReadOnlyCollection<ISpecification> specs, RuleOperator ruleOperator) =>
        ruleOperator switch
        {
            RuleOperator.And => new AndSpecification(specs),
            RuleOperator.Or => new OrSpecification(specs),
            _ => throw new NotSupportedException($"Unhandled rule operator: {ruleOperator}")
        };

    private static ISpecification BuildRuleSpecification(PatternRule rule)
    {
        var targetPositions = rule.TargetPositions;

        var referencePosition = rule.ReferencePosition;

        var length = rule.Length;

        var values = rule.Values.ToList();

        return rule.RuleType switch
        {
            PatternRuleType.EqualsPosition =>
                new EqualsPositionSpecification(targetPositions,
                                                referencePosition.GetValueOrDefault(),
                                                length,
                                                negate: false),

            PatternRuleType.NotEqualsPosition =>
                new EqualsPositionSpecification(targetPositions,
                                                referencePosition.GetValueOrDefault(),
                                                length,
                                                negate: true),

            PatternRuleType.GreaterThanPosition =>
                new ComparePositionSpecification(targetPositions,
                                                 referencePosition.GetValueOrDefault(),
                                                 length,
                                                 greaterThan: true),

            PatternRuleType.LessThanPosition =>
                new ComparePositionSpecification(targetPositions,
                                                 referencePosition.GetValueOrDefault(),
                                                 length,
                                                 greaterThan: false),

            PatternRuleType.GreaterThanPrevious =>
                new CompareToPreviousSpecification(targetPositions, length, greaterThan: true),

            PatternRuleType.LessThanPrevious =>
                new CompareToPreviousSpecification(targetPositions, length, greaterThan: false),

            PatternRuleType.ValueWhitelist =>
                new ValueSetSpecification(targetPositions, values, length, blacklist: false),

            PatternRuleType.ValueBlacklist =>
                new ValueSetSpecification(targetPositions, values, length, blacklist: true),

            // PatternRuleType.ContainsRepeatedSubstring =>
            //     new ContainsRepeatedSubstringSpecification(values.Count > 0 ? values[0].Length : 2),

            _ => throw new NotSupportedException($"Unhandled rule type: {rule.RuleType}")
        };
    }
}
