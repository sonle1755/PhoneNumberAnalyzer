import { Container, Typography, Alert } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { PatternTemplateForm } from "../components/PatternTemplateForm";
import { useCreatePatternTemplate } from "../hooks/useCreatePatternTemplate";
import type { PatternTemplateFormValue } from "../types";
import { toCreateCommand } from "../mappers";

export function PatternTemplateCreatePage() {
  const navigate = useNavigate();
  const { mutate, isPending: isSaving, error } = useCreatePatternTemplate();

  const handleSubmit = (value: PatternTemplateFormValue) => {
    mutate(toCreateCommand(value), {
      onSuccess: (created) => navigate(`/pattern-templates/${created.id}`),
    });
  };

  return (
    <Container maxWidth="md" sx={{ py: 6 }}>
      <Typography variant="h4" gutterBottom>
        New Pattern Template
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          Failed to create template.
        </Alert>
      )}

      <PatternTemplateForm
        isSubmitting={isSaving}
        handleSubmit={handleSubmit}
      />
    </Container>
  );
}
