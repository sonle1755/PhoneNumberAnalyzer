using PhoneNumberAnalyzer.Business.Specifications;
using PhoneNumberAnalyzer.Data.Entities;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Business.Tests.Specifications;

public class PatternRuleGroupSpecificationBuilderTests
{
    [Fact]
    public void Build_SingleRuleGroup_AndOperator_EvaluatesCorrectly()
    {
        var group = new PatternRuleGroup
        {
            RuleOperator = RuleOperator.And,
            Rules =
            [
                new()
                {
                    RuleType = PatternRuleType.ValueWhitelist,
                    TargetPositions = [7,8],
                    Length = 2,
                    Values =  ["28"],
                }
            ]
        };

        var spec = PatternRuleGroupSpecificationBuilder.Build(group);

        Assert.True(spec.IsSatisfiedBy("0912345628"));
        Assert.False(spec.IsSatisfiedBy("0912345678"));
    }

    [Fact]
    public void Build_NestedChildGroups_OrOperator_EvaluatesCorrectly()
    {
        var childA = new PatternRuleGroup
        {
            RuleOperator = RuleOperator.And,
            Rules =
            [
                new()
                {
                    RuleType = PatternRuleType.ValueWhitelist,
                    TargetPositions= [9],
                    Length = 1,
                    Values = ["8"],
                }
            ]
        };

        var childB = new PatternRuleGroup
        {
            RuleOperator = RuleOperator.And,
            Rules =
            [
                new()
                {
                    RuleType = PatternRuleType.ValueWhitelist,
                    TargetPositions = [ 9],
                    Length = 1,
                    Values = ["9"],
                }
            ]
        };

        var root = new PatternRuleGroup
        {
            RuleOperator = RuleOperator.Or,
            ChildGroups = [childA, childB]
        };

        var spec = PatternRuleGroupSpecificationBuilder.Build(root);

        Assert.True(spec.IsSatisfiedBy("0912345678")); // ends in 8 -> matches childA
        Assert.True(spec.IsSatisfiedBy("0912345679")); // ends in 9 -> matches childB
        Assert.False(spec.IsSatisfiedBy("0912345670")); // ends in 0 -> matches neither
    }

    [Fact]
    public void Build_GroupWithNeitherRulesNorChildGroups_ThrowsInvalidOperationException()
    {
        var group = new PatternRuleGroup { RuleOperator = RuleOperator.And };

        Assert.Throws<InvalidOperationException>(() => PatternRuleGroupSpecificationBuilder.Build(group));
    }
}
