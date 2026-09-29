import { PatternRuleType } from "@/client";

export const ruleTypeLabels: Record<PatternRuleType, string> = {
  [PatternRuleType.EQUALS_POSITION]: "Equals Position",
  [PatternRuleType.NOT_EQUALS_POSITION]: "Not Equals Position",
  [PatternRuleType.GREATER_THAN_POSITION]: "Greater Than Position",
  [PatternRuleType.LESS_THAN_POSITION]: "Less Than Position",
  [PatternRuleType.GREATER_THAN_PREVIOUS]: "Greater Than Previous",
  [PatternRuleType.LESS_THAN_PREVIOUS]: "Less Than Previous",
  [PatternRuleType.VALUE_WHITELIST]: "Value Whitelist",
  [PatternRuleType.VALUE_BLACKLIST]: "Value Blacklist",
  [PatternRuleType.CONTAINS_REPEATED_SUBSTRING]: "Contains Repeated Substring",
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
