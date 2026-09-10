import { useNavigate, useParams } from "react-router-dom";
import { useGetPatternTemplateById } from "../hooks/useGetPatternTemplateById";
import { PatternTemplateForm } from "../components/PatternTemplateForm";
import { Alert, Container, Typography } from "@mui/material";
import type { PatternTemplateFormValue } from "../types";
import { toUpdateCommand } from "../mappers";
import { useUpdatePatternTemplate } from "../hooks/useUpdatePatternTemplate";

export function PatternTemplateEditPage() {
  const { patternTemplateId } = useParams();
  const navigate = useNavigate();
  const {
    data,
    error: fetchError,
    isPending: isFetching,
  } = useGetPatternTemplateById(patternTemplateId);

  const { mutate, isPending: isSaving } =
    useUpdatePatternTemplate(patternTemplateId);
  const handleSubmit = (value: PatternTemplateFormValue) => {
    mutate(toUpdateCommand(value), {
      onSuccess: () => navigate(`/pattern-templates/${patternTemplateId}`),
    });
  };

  if (isFetching) return <>Fetching data...</>;
  else if (fetchError)
    return (
      <Alert severity="error" sx={{ mb: 2 }}>
        Failed to fetch pattern template.
      </Alert>
    );
  else
    return (
      <Container maxWidth="md" sx={{ py: 6 }}>
        <Typography variant="h4" gutterBottom>
          Edit Pattern Template: {data.name}
        </Typography>

        <PatternTemplateForm
          key={patternTemplateId}
          isSubmitting={isSaving}
          handleSubmit={handleSubmit}
          patternTemplate={data}
        />
      </Container>
    );
}
