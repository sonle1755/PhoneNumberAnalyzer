import {
  Box,
  Collapse,
  IconButton,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import type { PhoneAnalysisResult } from "@/shared/api/Api";
import { Fragment, useId, useState } from "react";
import KeyboardArrowDownIcon from "@mui/icons-material/KeyboardArrowDown";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";

interface AnalysisResultProps {
  result: PhoneAnalysisResult[];
}

function ResultRow(props: { row: PhoneAnalysisResult }) {
  const { row } = props;
  const [open, setOpen] = useState(false);
  const detailsId = useId();

  return (
    <Fragment>
      <TableRow sx={{ "& > .MuiTableCell-root": { borderBottom: "unset" } }}>
        <TableCell>
          <IconButton
            aria-label={open ? "collapse row" : "expand row"}
            aria-expanded={open}
            aria-controls={detailsId}
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.originalInput}
        </TableCell>
        <TableCell>{row.normalizedDigits}</TableCell>
        <TableCell>{row.matchCount}</TableCell>
      </TableRow>
      <TableRow id={detailsId} aria-hidden={!open ? true : undefined}>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Matched Patterns
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell>Date</TableCell>
                    <TableCell>Customer</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.matchedPatterns.map((p) => (
                    <TableRow key={p.templateId}>
                      <TableCell component="th" scope="row">
                        {p.templateId}
                      </TableCell>
                      <TableCell>{p.templateName}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </Fragment>
  );
}

export function AnalysisResult({ result }: AnalysisResultProps) {
  const data = result.map((i) => {
    return {
      ...i,
      matchCount: i.matchedPatterns.length,
    };
  });
  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow>
            <TableCell />
            <TableCell>Original</TableCell>
            <TableCell>Normalized</TableCell>
            <TableCell>Match Count</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <ResultRow key={row.originalInput} row={row} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
