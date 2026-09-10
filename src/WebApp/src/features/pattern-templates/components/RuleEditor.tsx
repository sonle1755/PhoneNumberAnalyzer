import { useState } from "react";
import {
  Box,
  Stack,
  TextField,
  MenuItem,
  Typography,
  FormControl,
  InputLabel,
  OutlinedInput,
  Chip,
  Button,
} from "@mui/material";
import Select from "@mui/material/Select";
import type { PatternRuleFormModel } from "../types";
import { ruleTypeOptions, positionOptions } from "../constants";
import { PatternRuleType } from "@/shared/api/Api";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 420,
  maxWidth: "90vw",
  bgcolor: "background.paper",
  borderRadius: 2,
  boxShadow: 24,
  p: 4,
};

interface RuleEditorProps {
  initialRule: PatternRuleFormModel;
  onSubmit: (rule: PatternRuleFormModel) => void;
}

export function RuleEditor({ initialRule, onSubmit }: RuleEditorProps) {
  const [ruleName, setRuleName] = useState<string>(initialRule.name);
  const [ruleType, setRuleType] = useState<PatternRuleType>(
    initialRule.ruleType,
  );
  const [length, setLength] = useState<number>(initialRule.length);
  const [values, setValues] = useState<string[]>(initialRule.values);
  const [tPositions, setTPositions] = useState(initialRule.targetPositions);
  const [rPosition, setRPosition] = useState(initialRule.referencePosition);

  const [inputValue, setInputValue] = useState("");

  const handleTPositionChange = (e) => {
    const { value } = e.target;
    setTPositions(
      typeof value === "string" ? value.split(",").map(Number) : value,
    );
  };

  const handleKeyDown = (e) => {
    if ((e.key === "Enter" || e.key === ",") && inputValue.trim()) {
      e.preventDefault();
      if (!values.includes(inputValue.trim())) {
        setValues([...values, inputValue.trim()]);
      }
      setInputValue("");
    } else if (e.key === "Backspace" && !inputValue && values.length > 0) {
      setValues(values.slice(0, -1));
    }
  };

  const handleDelete = (chipToDelete) => {
    setValues(values.filter((v) => v !== chipToDelete));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({
      tempId: initialRule.tempId,
      name: ruleName,
      ruleType: ruleType,
      length: length,
      targetPositions: tPositions,
      referencePosition: rPosition ? rPosition : null,
      values: values,
    });
  };
  return (
    <Box sx={style}>
      <Typography variant="h4" sx={{ mb: 2 }}>
        New Rule
      </Typography>
      <Stack spacing={2}>
        <TextField
          label="Name"
          value={ruleName}
          onChange={(e) => setRuleName(e.target.value)}
        />
        <TextField
          select
          label="Rule type"
          value={ruleType}
          onChange={(e) =>
            setRuleType(Number(e.target.value) as PatternRuleType)
          }
        >
          {ruleTypeOptions.map((opt) => (
            <MenuItem key={opt.value} value={opt.value}>
              {opt.label}
            </MenuItem>
          ))}
        </TextField>

        <TextField
          label="Length"
          value={length}
          onChange={(e) => setLength(Number(e.target.value))}
        />

        <TextField
          fullWidth
          label="Values"
          placeholder="Type and press Enter"
          value={inputValue}
          onChange={(e) => {
            setInputValue(e.target.value);
          }}
          onKeyDown={handleKeyDown}
          slotProps={{
            input: {
              startAdornment: values.map((value) => (
                <Chip
                  key={value}
                  label={value}
                  size="small"
                  onDelete={() => handleDelete(value)}
                  sx={{ m: 0.3 }}
                />
              )),
            },
          }}
        />

        <FormControl>
          <InputLabel id="multi-select-target-position-label">
            Target Positions
          </InputLabel>
          <Select
            labelId="multiple-target-select-label"
            id="multiple-target-select"
            multiple
            value={tPositions}
            onChange={handleTPositionChange}
            input={
              <OutlinedInput
                id="select-multiple-target-position"
                label="Target positions"
              />
            }
            renderValue={(selected) => (
              <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                {selected.map((value) => (
                  <Chip key={value} label={value} />
                ))}
              </Box>
            )}
          >
            {positionOptions.map((opt) => (
              <MenuItem key={opt.value} value={opt.label}>
                {opt.label}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <FormControl>
          <InputLabel id="input-reference-position-label">
            Reference Position
          </InputLabel>
          <Select
            labelId="input-reference-position-label"
            label="Reference Position"
            id="reference-position-select"
            value={rPosition}
            onChange={(e) => setRPosition(e.target.value)}
          >
            <MenuItem value="0">
              <em>None</em>
            </MenuItem>
            {positionOptions.map((opt) => (
              <MenuItem key={opt.value} value={opt.label}>
                {opt.label}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <Button variant="contained" onClick={handleSubmit}>
          Save
        </Button>
      </Stack>
    </Box>
  );
}
