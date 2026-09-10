namespace PhoneNumberAnalyzer.Business.Specifications.Composite;

/// <summary>
/// Composite: satisfied if at least one child specification is satisfied.
/// Used for a PatternRuleGroup with RuleOperator = Or.
/// </summary>
public sealed class OrSpecification : ISpecification
{
    private readonly IReadOnlyCollection<ISpecification> _children;

    public OrSpecification(IReadOnlyCollection<ISpecification> children)
    {
        _children = children;
    }

    // Empty children = vacuously false (nothing present to satisfy on)
    public bool IsSatisfiedBy(string digits) => _children.Any(c => c.IsSatisfiedBy(digits));
}
