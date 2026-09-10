using PhoneNumberAnalyzer.Business.Specifications.Leaf;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Leaf;

public class CompareToPreviousSpecificationTests
{
    [Fact]
    public void IsSatisfiedBy_GreaterThanPrevious_ReturnsTrue()
    {
        var spec = new CompareToPreviousSpecification(
            targetPositions: [3],
            length: 1,
            greaterThan: true);

        // digits[2]='1', digits[3]='9'
        var result = spec.IsSatisfiedBy("0919345678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_NotGreaterThanPrevious_ReturnsFalse()
    {
        var spec = new CompareToPreviousSpecification(
            targetPositions: [3],
            length: 1,
            greaterThan: true);

        // digits[2]='9', digits[3]='1'
        var result = spec.IsSatisfiedBy("0991345678");

        Assert.False(result);
    }

    [Fact]
    public void IsSatisfiedBy_LessThanPrevious_ReturnsTrue()
    {
        var spec = new CompareToPreviousSpecification(
            targetPositions: [3],
            length: 1,
            greaterThan: false);

        // digits[2]='9', digits[3]='1'
        var result = spec.IsSatisfiedBy("0991345678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_PositionOne_HasNoPrevious_ReturnsFalse()
    {
        var spec = new CompareToPreviousSpecification(
            targetPositions: [1],
            length: 1,
            greaterThan: true);

        var result = spec.IsSatisfiedBy("0912345678");

        Assert.False(result);
    }
}
