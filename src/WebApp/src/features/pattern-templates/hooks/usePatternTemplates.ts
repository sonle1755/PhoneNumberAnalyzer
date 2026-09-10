import { useQuery } from "@tanstack/react-query";
import { getPatternTemplates } from "../api/patternTemplateApi";

export function usePatternTemplates() {
  return useQuery({
    queryKey: ["pattern-templates"],
    queryFn: getPatternTemplates,
  });
}
