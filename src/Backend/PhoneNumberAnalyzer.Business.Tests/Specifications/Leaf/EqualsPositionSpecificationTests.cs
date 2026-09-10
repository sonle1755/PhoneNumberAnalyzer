using PhoneNumberAnalyzer.Business.Specifications.Leaf;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Leaf;

public class EqualsPositionSpecificationTests
{
    [Fact]
    public void IsSatisfiedBy_TargetEqualsReference_ReturnsTrue()
    {
        var spec = new EqualsPositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            negate: false);

        // digits[3] = '2', digits[6] = '2'
        var result = spec.IsSatisfiedBy("0912542678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_TargetNotEqualsReference_ReturnsFalse()
    {
        var spec = new EqualsPositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            negate: false);

        var result = spec.IsSatisfiedBy("0912345678");

        Assert.False(result);
    }

    [Fact]
    public void IsSatisfiedBy_Negated_TargetNotEqualsReference_ReturnsTrue()
    {
        var spec = new EqualsPositionSpecification(
            targetPositions: [3],
            referencePosition: 6,
            length: 1,
            negate: true);

        var result = spec.IsSatisfiedBy("0912345678");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_MultipleTargetsAndReferences_AllPairsMustMatch()
    {
        var spec = new EqualsPositionSpecification(
            targetPositions: [3, 4],
            referencePosition: 6,
            length: 1,
            negate: false);

        // digits[3]='2', digits[4]='5', digits[6]='5' - target 4 doesn't match reference 6
        var result = spec.IsSatisfiedBy("0912545678");

        Assert.False(result);
    }
}
