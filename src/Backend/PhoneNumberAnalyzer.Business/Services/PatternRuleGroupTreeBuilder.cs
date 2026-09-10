using PhoneNumberAnalyzer.Data.Entities;

namespace PhoneNumberAnalyzer.Business.Services;

public static class PatternRuleGroupTreeBuilder
{
    public static void AttachChildGroups(ICollection<PatternRuleGroup> allGroups)
    {
        var groupsById = allGroups.ToDictionary(g => g.Id);

        foreach (var group in allGroups)
        {
            if (group.ParentId is { } parentId && groupsById.TryGetValue(parentId, out var parent))
            {
                parent.ChildGroups.Add(group);
            }
        }
    }

    public static PatternRuleGroup GetRootGroup(PatternTemplate template) =>
        template.RuleGroups.Single(g => g.ParentId == null && g.Level == 0);
}
