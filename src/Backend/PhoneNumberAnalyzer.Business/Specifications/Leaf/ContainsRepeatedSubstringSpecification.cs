namespace PhoneNumberAnalyzer.Business.Specifications.Leaf;

/// <summary>
/// RuleType: ContainsRepeatedSubstring. True if any window of the given
/// length appears more than once anywhere in the number.
/// </summary>
public sealed class ContainsRepeatedSubstringSpecification : ISpecification
{
    private readonly int _length;

    public ContainsRepeatedSubstringSpecification(int length) => _length = length;

    public bool IsSatisfiedBy(string digits)
    {
        if (_length <= 0 || digits.Length < _length * 2) return false;

        var seen = new HashSet<string>();
        for (int i = 0; i <= digits.Length - _length; i++)
        {
            var window = digits.Substring(i, _length);
            if (!seen.Add(window)) return true; // already seen this window before = repeated
        }

        return false;
    }
}
