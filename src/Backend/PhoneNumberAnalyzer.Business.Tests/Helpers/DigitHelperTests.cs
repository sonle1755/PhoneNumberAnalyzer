using PhoneNumberAnalyzer.Business.Helpers;

namespace PhoneNumberAnalyzer.Business.Tests.Helpers;

public class DigitHelperTests
{
    [Fact]
    public void DigitAt_SingleDigit_ReturnsCorrectValue()
    {
        var result = DigitHelper.DigitAt("0912345678", position: 3, length: 1);

        Assert.Equal(2, result);
    }

    [Fact]
    public void DigitAt_MultiDigitSpan_ReturnsCorrectValue()
    {
        var result = DigitHelper.DigitAt("0912345678", position: 3, length: 2);

        Assert.Equal(23, result);
    }

    [Fact]
    public void DigitAt_AtStartOfString_ReturnsCorrectValue()
    {
        var result = DigitHelper.DigitAt("0912345678", position: 0, length: 1);

        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData("0912345678", 1, 1, 9)]
    [InlineData("0912345678", 9, 1, 8)]
    [InlineData("0912345678", 5, 3, 456)]
    public void DigitAt_VariousPositionsAndLengths_ReturnsExpectedValue(
        string digits, int position, int length, int expected)
    {
        var result = DigitHelper.DigitAt(digits, position, length);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DigitAt_PositionOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            DigitHelper.DigitAt("0912345678", position: 15, length: 1));
    }

    [Fact]
    public void DigitAt_LengthExceedsRemainingString_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            DigitHelper.DigitAt("0912345678", position: 8, length: 5));
    }
}
