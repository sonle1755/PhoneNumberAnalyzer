using Microsoft.EntityFrameworkCore;
using PhoneNumberAnalyzer.Data.Enums;
using PhoneNumberAnalyzer.Data.Interfaces;

namespace PhoneNumberAnalyzer.Data.Repositories;

public class PatternRuleGroupRepository(AppDbContext db) : IPatternRuleGroupRepository
{
    private readonly AppDbContext _db = db;

    public async Task UpdateAsync(Guid groupId, Guid templateId, string name, RuleOperator ruleOperator)
    {
        var group = await _db
            .PatternRuleGroups
            .Where(g => g.Id == groupId && g.PatternTemplateId == templateId)
            .FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(groupId));

        group.Name = name;
        group.RuleOperator = ruleOperator;
    }
}
