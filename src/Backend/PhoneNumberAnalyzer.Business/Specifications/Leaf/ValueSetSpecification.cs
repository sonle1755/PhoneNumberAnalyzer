using System.Diagnostics.CodeAnalysis;

namespace PhoneNumberAnalyzer.Business.Specifications.Leaf;

/// <summary>
/// RuleType: ValueWhitelist / ValueBlacklist. If targetPositions is null/empty,
/// checks every contiguous window of the value length anywhere in the number.
/// </summary>
public sealed class ValueSetSpecification : ISpecification
{
    private readonly IReadOnlyCollection<int> _targetPositions;
    private readonly HashSet<string> _values;
    private readonly int _length;
    private readonly bool _blacklist;

    public ValueSetSpecification(
        [NotNull] IReadOnlyCollection<int> targetPositions,
        [NotNull] IReadOnlyCollection<string> values,
        [NotNull] int length,
        bool blacklist)
    {
        ArgumentNullException.ThrowIfNull(values);
        _targetPositions = targetPositions ?? throw new ArgumentNullException(nameof(targetPositions));
        _values = [.. values];
        _length = length;
        _blacklist = blacklist;
    }

    public bool IsSatisfiedBy(string digits)
    {
        var ordered = _targetPositions.Order();
        var targetDigits = ordered.Select(p => digits.Substring(p, _length));
        if (targetDigits is null) return false;

        var isMember = targetDigits.Any(td => _values.Any(v => td.Contains(v)));
        return _blacklist ? !isMember : isMember;
    }
}
