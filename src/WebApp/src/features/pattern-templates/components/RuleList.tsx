import {
  Box,
  Chip,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Paper,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { ruleTypeOptions } from "../constants";
import type { FormPatternRule } from "../types";

interface RuleListProps {
  rules: FormPatternRule[];
  handleEdit: (rule: FormPatternRule) => void;
  handleDelete: (tempId: string) => void;
}

export function RuleList({ rules, handleEdit, handleDelete }: RuleListProps) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="Rule Table">
        <TableBody>
          {rules.map((r, index) => (
            <TableRow
              key={r.tempId}
              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {r.name}
              </TableCell>
              <TableCell component="th" scope="row">
                {r.length}
              </TableCell>
              <TableCell align="right">
                <Chip
                  key={r.tempId + "-" + r.ruleType + "-" + index}
                  label={ruleTypeOptions[r.ruleType].label}
                  size="small"
                />
              </TableCell>
              <TableCell align="right">
                <Box>
                  <EditIcon
                    onClick={() => handleEdit(r)}
                    fontSize="small"
                    color="action"
                  />
                  <DeleteIcon
                    onClick={() => handleDelete(r.tempId)}
                    fontSize="small"
                    color="error"
                  />
                </Box>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
