using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Business.Interfaces;

public interface IPatternSpecificationCache
{
    ISpecification GetOrBuild(PatternTemplate template);
}
