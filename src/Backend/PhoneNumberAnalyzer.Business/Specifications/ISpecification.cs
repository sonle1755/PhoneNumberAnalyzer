namespace PhoneNumberAnalyzer.Business.Specifications;

public interface ISpecification
{
    bool IsSatisfiedBy(string digits);
}
