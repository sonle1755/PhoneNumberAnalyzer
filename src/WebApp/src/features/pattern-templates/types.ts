import { RuleOperator, type PatternRuleType } from "@/shared/api/Api";

export const PatternRuleGroupMode = {
  Groups: 0,
  Rules: 1,
} as const;

export type PatternRuleGroupMode =
  (typeof PatternRuleGroupMode)[keyof typeof PatternRuleGroupMode];

export interface PatternRuleFormModel {
  tempId: string; // client-only, for React keys - not sent to backend
  name: string;
  length: number;
  ruleType: PatternRuleType;
  targetPositions: number[];
  referencePosition: number | null;
  values: string[];
}

export interface PatternRuleGroupFormModel {
  tempId: string;
  name: string;
  ruleOperator: RuleOperator;
  mode: PatternRuleGroupMode;
  rules: PatternRuleFormModel[];
  childGroups: PatternRuleGroupFormModel[];
}

export interface PatternTemplateFormValue {
  name: string;
  description: string;
  patternRuleGroup: PatternRuleGroupFormModel;
}

export function createEmptyRootGroup(): PatternRuleGroupFormModel {
  return {
    tempId: crypto.randomUUID(),
    name: "Root",
    ruleOperator: RuleOperator.And,
    mode: PatternRuleGroupMode.Rules,
    rules: [],
    childGroups: [],
  };
}

export function createEmptyFormValue(): PatternTemplateFormValue {
  return {
    name: "",
    description: "",
    patternRuleGroup: createEmptyRootGroup(),
  };
}
