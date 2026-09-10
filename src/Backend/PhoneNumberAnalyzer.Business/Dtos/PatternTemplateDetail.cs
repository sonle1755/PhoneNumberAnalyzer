using System.Collections.Immutable;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Business.Dtos;

public record PatternTemplateDetail(Guid Id,
                                    string Name,
                                    string Description,
                                    PatternRuleGroupDetail PatternRuleGroup);

public record PatternRuleGroupDetail(Guid Id,
                                     string Name,
                                     int Level,
                                     bool IsRoot,
                                     RuleOperator RuleOperator,
                                     ImmutableArray<PatternRuleDetail> Rules,
                                     ImmutableArray<PatternRuleGroupDetail> ChildGroups);

public record PatternRuleDetail(Guid Id,
                                string Name,
                                PatternRuleType RuleType,
                                int Length,
                                ImmutableArray<string> Values,
                                ImmutableArray<int> TargetPositions,
                                int? ReferencePosition);
