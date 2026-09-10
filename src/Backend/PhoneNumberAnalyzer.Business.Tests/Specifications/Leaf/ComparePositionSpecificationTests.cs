using PhoneNumberAnalyzer.Business.Specifications.Leaf;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Leaf;

public class ComparePositionSpecificationTests
{
    [Fact]
    public void IsSatisfiedBy_GreaterThan_TargetGreaterThanReference_ReturnsTrue()
    {
        var spec = new ComparePositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            greaterThan: true);

        // digits[3]='9', digits[6]='5'
        var result = spec.IsSatisfiedBy("0919545678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_GreaterThan_TargetNotGreaterThanReference_ReturnsFalse()
    {
        var spec = new ComparePositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            greaterThan: true);

        // digits[3]='1', digits[6]='5'
        var result = spec.IsSatisfiedBy("0911545678");

        Assert.False(result);
    }

    [Fact]
    public void IsSatisfiedBy_LessThan_TargetLessThanReference_ReturnsTrue()
    {
        var spec = new ComparePositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            greaterThan: false);

        // digits[3]='1', digits[6]='5'
        var result = spec.IsSatisfiedBy("0911545678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_EqualValues_GreaterThanReturnsFalse()
    {
        var spec = new ComparePositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            greaterThan: true);

        // digits[3]='5', digits[6]='5'
        var result = spec.IsSatisfiedBy("0915545678");

        Assert.False(result);
    }
}
