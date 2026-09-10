using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Business.Specifications.Composite;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Composite;

public class OrSpecificationTests
{
    private sealed class StubSpecification : ISpecification
    {
        private readonly bool _result;
        public StubSpecification(bool result) => _result = result;
        public bool IsSatisfiedBy(string digits) => _result;
    }

    [Fact]
    public void IsSatisfiedBy_OneChildTrue_ReturnsTrue()
    {
        var spec = new OrSpecification(
        [
            new StubSpecification(false),
            new StubSpecification(true),
        ]);

        Assert.True(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void IsSatisfiedBy_AllChildrenFalse_ReturnsFalse()
    {
        var spec = new OrSpecification(
        [
            new StubSpecification(false),
            new StubSpecification(false),
        ]);

        Assert.False(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void IsSatisfiedBy_EmptyChildren_ReturnsFalse()
    {
        var spec = new OrSpecification([]);

        Assert.False(spec.IsSatisfiedBy("0912345678"));
    }
}
