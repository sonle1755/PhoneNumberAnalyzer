namespace PhoneNumberAnalyzer.Business.Helpers;

public static class DigitHelper
{
    public static int DigitAt(string digits, int position, int length) =>
        int.Parse(digits.Substring(position, length));
}
