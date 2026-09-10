using System.Collections.Immutable;
using PhoneNumberAnalyzer.Data.Enums;

namespace PhoneNumberAnalyzer.Business.Dtos;

public record PatternTemplateCreateCommand(string Name,
                                           string Description,
                                           PatternRuleGroupCreateCommand PatternRuleGroup);

public record PatternRuleGroupCreateCommand(string Name,
                                            RuleOperator RuleOperator,
                                            ImmutableArray<PatternRuleGroupCreateCommand> ChildGroups,
                                            ImmutableArray<PatternRuleCreateCommand> Rules);

public record PatternRuleCreateCommand(string Name,
                                       PatternRuleType RuleType,
                                       int Length,
                                       ImmutableArray<string> Values,
                                       ImmutableArray<int> TargetPositions,
                                       int? ReferencePosition);
