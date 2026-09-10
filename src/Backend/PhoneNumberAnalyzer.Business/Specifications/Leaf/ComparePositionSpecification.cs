using System.Diagnostics.CodeAnalysis;
using PhoneNumberAnalyzer.Business.Helpers;

namespace PhoneNumberAnalyzer.Business.Specifications.Leaf;

/// <summary>
/// RuleType: GreaterThanPosition / LessThanPosition.
/// </summary>
public sealed class ComparePositionSpecification : ISpecification
{
    private readonly IReadOnlyCollection<int> _targetPositions;
    private readonly int _referencePosition;
    private readonly int _length;
    private readonly bool _greaterThan;

    public ComparePositionSpecification([NotNull] IReadOnlyCollection<int> targetPositions,
                                        [NotNull] int referencePosition,
                                        [NotNull] int length,
                                        bool greaterThan)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(referencePosition);
        _targetPositions = targetPositions ?? throw new ArgumentNullException(nameof(targetPositions));
        _referencePosition = referencePosition;
        _greaterThan = greaterThan;
        _length = length;
    }

    public bool IsSatisfiedBy(string digits) =>
        _targetPositions.All(t =>
                _greaterThan
                    ? DigitHelper.DigitAt(digits, t, _length) > DigitHelper.DigitAt(digits, _referencePosition, _length)
                    : DigitHelper.DigitAt(digits, t, _length) < DigitHelper.DigitAt(digits, _referencePosition, _length));
}
