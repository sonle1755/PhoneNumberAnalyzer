using System.Text.RegularExpressions;
using PhoneNumberAnalyzer.Business.Dtos;
using PhoneNumberAnalyzer.Business.Interfaces;

namespace PhoneNumberAnalyzer.Business.Services;

public partial class PhoneNumberExtractionService : IPhoneNumberExtractionService
{
    // Matches a run of digit/O/o characters, optionally interleaved with
    // separator characters (. - or whitespace), as long as it starts and
    // ends on a digit-like character. Doesn't pre-assume group sizes.
    [GeneratedRegex(@"[0-9Oo](?:[0-9Oo\.\-]*[0-9Oo])?")]
    private static partial Regex DigitRunPattern();

    [GeneratedRegex(@"^(0)(3[2-9]|5[2689]|7[06-9]|8[1-9]|9[0-9])\d{7}$")]
    private static partial Regex ValidPrefixPattern();

    public IReadOnlyCollection<ExtractedPhoneNumber> ExtractFromText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return [];

        var candidates = new List<ExtractedPhoneNumber>();

        foreach (Match match in DigitRunPattern().Matches(text))
        {
            var digitsOnly = NormalizeDigits(match.Value);

            if (IsPlausiblePhoneNumber(digitsOnly))
            {
                candidates.Add(new ExtractedPhoneNumber(match.Value, digitsOnly, match.Index));
            }
        }

        return [.. candidates
            .GroupBy(c => c.NormalizedDigits)
            .Select(g => g.OrderBy(c => c.StartIndex).First())
            .OrderBy(c => c.StartIndex)];
    }

    private static string NormalizeDigits(string raw) =>
        new([.. raw
            .Where(c => c is (>= '0' and <= '9') or 'O' or 'o')
            .Select(c => c is 'O' or 'o' ? '0' : c)]);

    private static bool IsPlausiblePhoneNumber(string digitsOnly) =>
        digitsOnly.Length == 10 && ValidPrefixPattern().IsMatch(digitsOnly);
}
