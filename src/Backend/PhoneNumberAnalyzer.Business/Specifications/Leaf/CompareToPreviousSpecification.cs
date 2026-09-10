using System.Diagnostics.CodeAnalysis;
using PhoneNumberAnalyzer.Business.Helpers;

namespace PhoneNumberAnalyzer.Business.Specifications.Leaf;

/// <summary>
/// RuleType: GreaterThanPrevious / LessThanPrevious. No reference position
/// needed - always compares a target digit to its immediate predecessor.
/// </summary>
public sealed class CompareToPreviousSpecification : ISpecification
{
    private readonly IReadOnlyCollection<int> _targetPositions;
    private readonly int _length;
    private readonly bool _greaterThan;

    public CompareToPreviousSpecification([NotNull] IReadOnlyCollection<int> targetPositions,
                                          [NotNull] int length,
                                          bool greaterThan)
    {
        _targetPositions = targetPositions ?? throw new ArgumentNullException(nameof(targetPositions));
        _length = length;
        _greaterThan = greaterThan;
    }

    public bool IsSatisfiedBy(string digits) =>
        _targetPositions.All(pos =>
        {
            if (pos <= 1) return false; // position 1 has no predecessor
            var current = DigitHelper.DigitAt(digits, pos, _length);
            var previous = DigitHelper.DigitAt(digits, pos - 1, _length);
            return _greaterThan ? current > previous : current < previous;
        });
}
