using System.Diagnostics.CodeAnalysis;

namespace PhoneNumberAnalyzer.Business.Specifications.Leaf;

/// <summary>
/// RuleType: EqualsPosition / NotEqualsPosition. Every target position is
/// compared against every reference position under this rule.
/// </summary>
public sealed class EqualsPositionSpecification : ISpecification
{
    private readonly IReadOnlyCollection<int> _targetPositions;
    private readonly int _referencePosition;
    private readonly int _length;
    private readonly bool _negate;

    public EqualsPositionSpecification(
        [NotNull] IReadOnlyCollection<int> targetPositions,
        [NotNull] int referencePosition,
        [NotNull] int length,
        bool negate = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(referencePosition);
        _targetPositions = targetPositions ?? throw new ArgumentNullException(nameof(targetPositions));
        _referencePosition = referencePosition;
        _length = length;
        _negate = negate;
    }

    // Don't care about case-insensitive so == is enough
    public bool IsSatisfiedBy(string digits) =>
        _targetPositions.All(t =>
                digits.Substring(t, _length) == digits.Substring(_referencePosition, _length) != _negate);
}
