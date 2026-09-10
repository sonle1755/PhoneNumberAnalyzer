using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Business.Specifications.Composite;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications.Composite;

public class AndSpecificationTests
{
    private sealed class StubSpecification : ISpecification
    {
        private readonly bool _result;
        public StubSpecification(bool result) => _result = result;
        public bool IsSatisfiedBy(string digits) => _result;
    }

    [Fact]
    public void IsSatisfiedBy_AllChildrenTrue_ReturnsTrue()
    {
        var spec = new AndSpecification(
        [
            new StubSpecification(true),
            new StubSpecification(true),
        ]);

        Assert.True(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void IsSatisfiedBy_OneChildFalse_ReturnsFalse()
    {
        var spec = new AndSpecification(
        [
            new StubSpecification(true),
            new StubSpecification(false),
        ]);

        Assert.False(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void IsSatisfiedBy_EmptyChildren_ReturnsTrue()
    {
        var spec = new AndSpecification([]);

        Assert.True(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void IsSatisfiedBy_NestedComposite_EvaluatesRecursively()
    {
        var inner = new AndSpecification([new StubSpecification(true)]);
        var outer = new AndSpecification([inner, new StubSpecification(true)]);

        Assert.True(outer.IsSatisfiedBy("0912345678"));
    }
}
