using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Data.Entities;

public class PatternRuleGroup
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Level { get; set; }

    public RuleOperator RuleOperator { get; set; }

    public Guid PatternTemplateId { get; set; }

    public PatternTemplate PatternTemplate { get; set; } = null!;

    public Guid? ParentId { get; set; }

    public PatternRuleGroup? ParentGroup { get; set; }

    public ICollection<PatternRuleGroup> ChildGroups { get; set; } = [];

    public ICollection<PatternRule> Rules { get; set; } = [];

}
