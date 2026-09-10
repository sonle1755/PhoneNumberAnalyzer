import { useQuery } from "@tanstack/react-query";
import { getPatternTemplateById } from "../api/patternTemplateApi";

export function useGetPatternTemplateById(patternTemplateId: string) {
  return useQuery({
    queryKey: ["pattern-templates", patternTemplateId],
    queryFn: () => getPatternTemplateById(patternTemplateId),
    enabled: !!patternTemplateId,
  });
}
