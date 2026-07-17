using System.Text.RegularExpressions;

namespace PhoneNumberAnalyzer.Business.Helpers;

// AI generated
public static partial class VietnamPhoneHelpers
{
    // Matches phones inside text (flexible: dot / space / raw)
    [GeneratedRegex(@"(?:\+84|0)(?:[ .]?\d){9}")]
    private static partial Regex ExtractRegex();

    // -----------------------------
    // Extract phone numbers
    // -----------------------------
    public static MatchCollection Extract(string input)
    {
        return ExtractRegex().Matches(input);
    }

    // -----------------------------
    // Normalize to 0 format
    // -----------------------------
    public static string? Normalize(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return null;

        var cleaned = NormalizeRaw(phone);

        // convert +84xxxxxxxxx → 0xxxxxxxxx
        if (cleaned.StartsWith("+84"))
            return "0" + cleaned[3..];

        return cleaned;
    }

    // -----------------------------
    // Internal cleanup
    // -----------------------------
    private static string NormalizeRaw(string input)
    {
        return input
            .Replace(" ", "")
            .Replace(".", "")
            .Trim();
    }
}

