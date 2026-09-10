import type {
  PatternRuleGroupFormModel,
  PatternTemplateFormValue,
  PatternRuleFormModel,
} from "./types";
import { PatternRuleGroupMode } from "./types";
import type {
  PatternTemplateCreateCommand,
  PatternRuleGroupCreateCommand,
  PatternRuleCreateCommand,
  PatternRuleGroupDetail,
  PatternTemplateDetail,
  PatternRuleDetail,
  PatternTemplateUpdateCommand,
} from "@/shared/api/Api";

function toRuleCommand(rule: PatternRuleFormModel): PatternRuleCreateCommand {
  return {
    name: rule.name,
    ruleType: rule.ruleType,
    length: rule.length,
    targetPositions: rule.targetPositions,
    referencePosition: rule.referencePosition,
    values: rule.values,
  };
}

function toGroupCommand(
  group: PatternRuleGroupFormModel,
): PatternRuleGroupCreateCommand {
  return {
    name: group.name || null,
    ruleOperator: group.ruleOperator,
    rules:
      group.mode === PatternRuleGroupMode.Rules
        ? group.rules.map(toRuleCommand)
        : [],
    childGroups:
      group.mode === PatternRuleGroupMode.Groups
        ? group.childGroups.map(toGroupCommand)
        : [],
  };
}

export function toCreateCommand(
  value: PatternTemplateFormValue,
): PatternTemplateCreateCommand {
  return {
    name: value.name,
    description: value.description || null,
    patternRuleGroup: toGroupCommand(value.patternRuleGroup),
  };
}

export function toUpdateCommand(
  value: PatternTemplateFormValue,
): PatternTemplateUpdateCommand {
  return {
    name: value.name,
    description: value.description || null,
  };
}

export function toRuleFormValue(rule: PatternRuleDetail): PatternRuleFormModel {
  return {
    tempId: rule.id,
    name: rule.name,
    length: rule.length,
    ruleType: rule.ruleType,
    targetPositions: rule.targetPositions,
    referencePosition: rule.referencePosition,
    values: rule.values,
  };
}

export function toGroupFormValue(
  group: PatternRuleGroupDetail,
): PatternRuleGroupFormModel {
  return {
    tempId: group.id,
    name: group.name,
    ruleOperator: group.ruleOperator,
    mode:
      group.childGroups.length > 0
        ? PatternRuleGroupMode.Groups
        : PatternRuleGroupMode.Rules,
    rules:
      group.rules.length > 0 ? group.rules.map((r) => toRuleFormValue(r)) : [],
    childGroups:
      group.childGroups.length > 0
        ? group.childGroups.map((g) => toGroupFormValue(g))
        : [],
  };
}

export function toFormValue(
  patternTemplate: PatternTemplateDetail,
): PatternTemplateFormValue {
  return {
    name: patternTemplate.name,
    description: patternTemplate.description,
    patternRuleGroup: toGroupFormValue(patternTemplate.patternRuleGroup),
  };
}
