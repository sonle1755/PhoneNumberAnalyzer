import { Container } from "@mui/material";
import { PhoneAnalyzerPage } from "../phone-analyzer";

export default function HomePage() {
  return (
    <Container maxWidth="md" sx={{ mt: 10, textAlign: "center" }}>
      <PhoneAnalyzerPage />
    </Container>
  );
}
