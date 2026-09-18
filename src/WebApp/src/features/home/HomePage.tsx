import { Container, Divider, type SxProps, type Theme } from "@mui/material";
import { PhoneNumberAnalyzer } from "./components/PhoneNumberAnalyzer";
import { HowItWorks } from "./components/HowItWorks";
import { Hero } from "./components/Hero";
import { Features } from "./components/Features";
import { CallToActionBanner } from "./components/CallToActionBanner";
import { useNavigate } from "react-router-dom";

export default function HomePage() {
  const navigate = useNavigate();
  const analyzerSectionId = "analyzer";
  const sectionSx: SxProps<Theme> = {
    pt: 10,
    pb: 10,
    pl: 10,
    pr: 10,
  };

  const heroSx: SxProps<Theme> = {
    ...sectionSx,
    pt: 12,
  };

  function handleRegister() {
    navigate("/register");
  }

  function handleGoToAnalyze() {
    document.getElementById(analyzerSectionId)?.scrollIntoView({
      behavior: "smooth",
    });
  }
  return (
    <Container disableGutters={true} sx={{ pb: 10 }}>
      <Hero sectionSx={heroSx} handleGoToAnalyze={handleGoToAnalyze} />
      <Divider />
      <PhoneNumberAnalyzer id={analyzerSectionId} sectionSx={sectionSx} />
      <Divider />
      <Features sectionSx={sectionSx} />
      <Divider />
      <HowItWorks sectionSx={sectionSx} />
      <CallToActionBanner
        sectionSx={sectionSx}
        handleRegister={handleRegister}
        handleGoToAnalyze={handleGoToAnalyze}
      />
    </Container>
  );
}
