import { useState } from "react";
import {
  Box,
  Paper,
  Stack,
  TextField,
  ToggleButton,
  ToggleButtonGroup,
  Button,
  IconButton,
  Typography,
  Divider,
  Modal,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import { RuleEditor } from "./RuleEditor";
import { RuleList } from "./RuleList";
import {
  type PatternRuleGroupFormModel,
  type PatternRuleFormModel,
  PatternRuleGroupMode,
} from "../types";
import { RuleOperator, PatternRuleType } from "@/shared/api/Api";

const MAX_DEPTH = 5; // mirrors backend's enforced max nesting depth

function createEmptyRule(): PatternRuleFormModel {
  return {
    tempId: crypto.randomUUID(),
    name: "",
    length: 1,
    ruleType: PatternRuleType.EqualsPosition,
    targetPositions: [],
    referencePosition: 0,
    values: [],
  };
}

function createEmptyChildGroup(): PatternRuleGroupFormModel {
  return {
    tempId: crypto.randomUUID(),
    name: "",
    ruleOperator: RuleOperator.And,
    mode: PatternRuleGroupMode.Rules,
    rules: [],
    childGroups: [],
  };
}

interface RuleGroupEditorProps {
  group: PatternRuleGroupFormModel;
  onChange: (group: PatternRuleGroupFormModel) => void;
  onRemove?: () => void; // absent for the root group - root cannot be removed
  isRoot?: boolean;
  depth?: number;
}

export function RuleGroupEditor({
  group,
  onChange,
  onRemove,
  isRoot = false,
  depth = 0,
}: RuleGroupEditorProps) {
  const [open, setOpen] = useState(false);
  const [draftRule, setDraftRule] = useState(createEmptyRule());
  const handleOperatorChange = (operator: RuleOperator | null) => {
    if (Object.values(RuleOperator).includes(operator as RuleOperator))
      onChange({ ...group, ruleOperator: operator });
  };

  const handleModeChange = (mode: PatternRuleGroupMode | null) => {
    if (mode) onChange({ ...group, mode, rules: [], childGroups: [] }); // clear the inactive collection to avoid stale data
  };

  const handleAddRule = (newRule: PatternRuleFormModel) => {
    onChange({ ...group, rules: [...group.rules, newRule] });
    setOpen(false);
    setDraftRule(createEmptyRule());
  };

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setDraftRule(createEmptyRule());
  };

  const handleUpdateRule = (rule: PatternRuleFormModel) => {
    setDraftRule(rule);
    setOpen(true);
    // onChange({
    //   ...group,
    //   rules: group.rules.map((r) => (r.tempId === tempId ? updatedRule : r)),
    // });
  };

  const handleRemoveRule = (tempId: string) => {
    onChange({
      ...group,
      rules: group.rules.filter((r) => r.tempId !== tempId),
    });
  };

  const handleAddChildGroup = () => {
    onChange({
      ...group,
      childGroups: [...group.childGroups, createEmptyChildGroup()],
    });
  };

  const handleUpdateChildGroup = (
    tempId: string,
    updatedGroup: PatternRuleGroupFormModel,
  ) => {
    onChange({
      ...group,
      childGroups: group.childGroups.map((g) =>
        g.tempId === tempId ? updatedGroup : g,
      ),
    });
  };

  const handleRemoveChildGroup = (tempId: string) => {
    onChange({
      ...group,
      childGroups: group.childGroups.filter((g) => g.tempId !== tempId),
    });
  };

  const canNestDeeper = depth < MAX_DEPTH - 1;

  return (
    <Paper
      variant="outlined"
      sx={{
        p: 2,
        ml: isRoot ? 0 : 2,
        borderLeft: isRoot ? undefined : "3px solid",
      }}
    >
      <Stack spacing={2}>
        <Stack
          direction="row"
          sx={{
            spacing: 2,
            alignItems: "center",
            justifyContent: "space-between",
          }}
        >
          <Stack direction="row" sx={{ spacing: 2, alignItems: "center" }}>
            <TextField
              label="Group name"
              size="small"
              value={group.name}
              onChange={(e) => onChange({ ...group, name: e.target.value })}
              sx={{ minWidth: 180 }}
            />

            <ToggleButtonGroup
              size="small"
              value={group.ruleOperator}
              exclusive
              onChange={(_, v) => handleOperatorChange(v)}
            >
              <ToggleButton value={RuleOperator.And}>AND</ToggleButton>
              <ToggleButton value={RuleOperator.Or}>OR</ToggleButton>
            </ToggleButtonGroup>
          </Stack>

          {!isRoot && onRemove && (
            <IconButton size="small" color="error" onClick={onRemove}>
              <DeleteIcon fontSize="small" />
            </IconButton>
          )}
        </Stack>

        <ToggleButtonGroup
          size="small"
          value={group.mode}
          exclusive
          onChange={(_, v) => handleModeChange(v)}
        >
          <ToggleButton value={PatternRuleGroupMode.Rules}>Rules</ToggleButton>
          <ToggleButton
            value={PatternRuleGroupMode.Groups}
            disabled={!canNestDeeper}
          >
            Nested Groups
          </ToggleButton>
        </ToggleButtonGroup>

        <Divider />

        {group.mode === PatternRuleGroupMode.Rules ? (
          <Stack spacing={2}>
            <Box sx={{ display: "flex" }}>
              <Button onClick={handleOpen}>Add Rule</Button>
              <Modal open={open} onClose={handleClose}>
                <RuleEditor initialRule={draftRule} onSubmit={handleAddRule} />
              </Modal>
            </Box>
            {group.rules.length === 0 && (
              <Typography variant="body2" color="text.secondary">
                No rules yet.
              </Typography>
            )}
            {
              <RuleList
                rules={group.rules}
                handleEdit={handleUpdateRule}
                handleDelete={handleRemoveRule}
              />
            }
          </Stack>
        ) : (
          <Stack spacing={2}>
            {group.childGroups.length === 0 && (
              <Typography variant="body2" color="text.secondary">
                No nested groups yet.
              </Typography>
            )}
            {group.childGroups.map((childGroup) => (
              <RuleGroupEditor
                key={childGroup.tempId}
                group={childGroup}
                onChange={(updated) =>
                  handleUpdateChildGroup(childGroup.tempId, updated)
                }
                onRemove={() => handleRemoveChildGroup(childGroup.tempId)}
                depth={depth + 1}
              />
            ))}
            <Button
              startIcon={<AddIcon />}
              onClick={handleAddChildGroup}
              size="small"
              sx={{ alignSelf: "flex-start" }}
            >
              Add Nested Group
            </Button>
          </Stack>
        )}
      </Stack>
    </Paper>
  );
}
