namespace PhoneNumberAnalyzer.Business.Specifications.Composite;

/// <summary>
/// Composite: satisfied only if every child specification is satisfied.
/// Used for a PatternRuleGroup with RuleOperator = And - its children may be
/// leaf rule specs or nested group composites, this class doesn't care which.
/// </summary>
public sealed class AndSpecification : ISpecification
{
    private readonly IReadOnlyCollection<ISpecification> _children;

    public AndSpecification(IReadOnlyCollection<ISpecification> children)
    {
        _children = children;
    }

    // Empty children = vacuously true (a group with nothing to check imposes no constraint)
    public bool IsSatisfiedBy(string digits) => _children.All(c => c.IsSatisfiedBy(digits));
}
