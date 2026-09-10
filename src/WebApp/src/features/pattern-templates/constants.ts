import { PatternRuleType } from "../../shared/api/Api";

export const ruleTypeLabels: Record<PatternRuleType, string> = {
  [PatternRuleType.EqualsPosition]: "Equals Position",
  [PatternRuleType.NotEqualsPosition]: "Not Equals Position",
  [PatternRuleType.GreaterThanPosition]: "Greater Than Position",
  [PatternRuleType.LessThanPosition]: "Less Than Position",
  [PatternRuleType.GreaterThanPrevious]: "Greater Than Previous",
  [PatternRuleType.LessThanPrevious]: "Less Than Previous",
  [PatternRuleType.ValueWhitelist]: "Value Whitelist",
  [PatternRuleType.ValueBlacklist]: "Value Blacklist",
  [PatternRuleType.ContainsRepeatedSubstring]: "Contains Repeated Substring",
};

export const ruleTypeOptions = Object.entries(ruleTypeLabels).map(
  ([value, label]) => ({
    value: Number(value) as PatternRuleType,
    label,
  }),
);

export const positionOptions = Array.from({ length: 9 }, (_, i) => ({
  value: i + 1,
  label: `${i + 1}`,
}));
