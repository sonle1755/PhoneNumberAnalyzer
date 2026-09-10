using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Data.Interfaces;

public interface IPatternRuleGroupRepository
{
    public Task UpdateAsync(Guid groupId, Guid templateId, string name, RuleOperator ruleOperator);
}
