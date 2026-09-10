import { Container, Box, Typography, Button, Alert } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { usePatternTemplates } from "../hooks/usePatternTemplates";
import PatternTemplateTable from "../components/PatternTemplateTable";

export function PatternTemplateListPage() {
  const { data, isLoading, error } = usePatternTemplates();
  const navigate = useNavigate();

  return (
    <Container maxWidth="lg" sx={{ py: 6 }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          mb: 3,
        }}
      >
        <Typography variant="h4">Pattern Templates</Typography>
        <Button
          variant="contained"
          onClick={() => navigate("/pattern-templates/new")}
        >
          New Template
        </Button>
      </Box>

      {error && (
        <Alert severity="error">Failed to load pattern templates.</Alert>
      )}
      <PatternTemplateTable
        templates={data ?? []}
        isLoading={isLoading}
        onRowClick={(id) => navigate(`/pattern-templates/${id}`)}
      />
    </Container>
  );
}
