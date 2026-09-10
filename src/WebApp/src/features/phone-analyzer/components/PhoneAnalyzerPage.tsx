import {
  Container,
  Typography,
  Alert,
  TextareaAutosize,
  Button,
} from "@mui/material";
import { useAnalyzePhoneNumber } from "../hooks/useAnalyzePhoneNumber";
import { AnalysisResult } from "./AnalysisResult";
import { useState } from "react";

export function PhoneAnalyzerPage() {
  const [value, setValue] = useState("");
  const { mutate, data, isPending, error } = useAnalyzePhoneNumber();

  const handleSubmit = () => {
    mutate(value);
  };

  if (isPending && !data) return <>Loading</>;
  else if (error && !data)
    return (
      <Alert severity="error" sx={{ mb: 2 }}>
        Failed to fetch pattern template.
      </Alert>
    );
  else
    return (
      <Container maxWidth="sm" sx={{ py: 6 }}>
        <Typography variant="h4" gutterBottom>
          Phone Number Analyzer
        </Typography>

        <TextareaAutosize
          id="phoneNumber-text"
          value={value}
          minRows={10}
          aria-label="maximum height"
          style={{ width: "100%" }}
          onChange={(e) => setValue(e.target.value)}
        />
        {error && (
          <Alert severity="error" sx={{ mt: 2 }} onClose={() => { }}>
            Something went wrong while analyzing this number. Please try again.
          </Alert>
        )}

        <Button variant="contained" onClick={handleSubmit} disabled={isPending}>
          Analyze
        </Button>
        {data && <AnalysisResult result={data} />}
      </Container>
    );
}
