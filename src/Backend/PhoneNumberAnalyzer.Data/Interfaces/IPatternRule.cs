using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IPatterRule
{
    Task<PatternRule> AddAsync();
}
