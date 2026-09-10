using PhoneNumberAnalyzer.Business.Specifications.Leaf;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Leaf;

public class ValueSetSpecificationTests
{
    [Fact]
    public void IsSatisfiedBy_Whitelist_MatchedSpanIsInSet_ReturnsTrue()
    {
        var spec = new ValueSetSpecification(
            targetPositions: [7, 8],
            values: ["28", "24", "49", "29"],
            length: 2,
            blacklist: false);

        var result = spec.IsSatisfiedBy("0912345628");

        Assert.True(result);
    }

    [Fact]
    public void IsSatisfiedBy_Whitelist_MatchedSpanNotInSet_ReturnsFalse()
    {
        var spec = new ValueSetSpecification(
            targetPositions: [7, 8],
            values: ["28", "24", "49", "29"],
            length: 2,
            blacklist: false);

        var result = spec.IsSatisfiedBy("0912345678");

        Assert.False(result);
    }
}
