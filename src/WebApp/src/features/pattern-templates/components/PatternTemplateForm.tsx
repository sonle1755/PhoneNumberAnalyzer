import {
  Button,
  Stack,
  TextField,
  Typography,
  Divider,
  Box,
} from "@mui/material";
import { RuleGroupEditor } from "./RuleGroupEditor";
import { createEmptyFormValue, type PatternTemplateFormValue } from "../types";
import type { PatternTemplateDetail } from "@/shared/api/Api";
import { useState } from "react";
import { toFormValue } from "../mappers";
import { useNavigate } from "react-router-dom";

interface PatternTemplateFormProps {
  isSubmitting: boolean;
  handleSubmit: (formData: PatternTemplateFormValue) => void;
  patternTemplate?: PatternTemplateDetail | null;
}

export function PatternTemplateForm({
  isSubmitting,
  handleSubmit,
  patternTemplate,
}: PatternTemplateFormProps) {
  const navigate = useNavigate();
  const [formData, setFormData] = useState(
    patternTemplate ? toFormValue(patternTemplate) : createEmptyFormValue(),
  );
  return (
    <Stack spacing={3}>
      <TextField
        label="Name"
        value={formData.name}
        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
        required
        fullWidth
      />
      <TextField
        label="Description"
        value={formData.description}
        onChange={(e) =>
          setFormData({ ...formData, description: e.target.value })
        }
        multiline
        minRows={2}
        fullWidth
      />

      <Divider />

      <Box>
        <Typography variant="subtitle1" gutterBottom>
          Rules
        </Typography>
        <RuleGroupEditor
          group={formData.patternRuleGroup}
          onChange={(patternRuleGroup) =>
            setFormData({ ...formData, patternRuleGroup })
          }
          isRoot
        />
      </Box>
      <Stack
        direction="row"
        spacing={2}
        sx={{
          mt: 4,
          justifyContent: "flex-end",
        }}
      >
        <Button onClick={() => navigate("/pattern-templates")}>Cancel</Button>
        <Button variant="contained" onClick={() => handleSubmit(formData)}>
          {isSubmitting ? "Saving..." : "Save"}
        </Button>
      </Stack>
    </Stack>
  );
}
