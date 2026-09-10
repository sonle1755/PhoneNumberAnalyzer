import { Chip } from "@mui/material";
import type { PatternTemplateMatch } from "../types";

interface MatchedPatternBadgeProps {
  match: PatternTemplateMatch;
}

export function MatchedPatternBadge({ match }: MatchedPatternBadgeProps) {
  return (
    <Chip
      label={`${match.templateName} +${match.weight}`}
      color="success"
      variant="outlined"
    />
  );
}
