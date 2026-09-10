import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePatternTemplate } from "../api/patternTemplateApi";
import type { PatternTemplateUpdateCommand } from "@/shared/api/Api";

export function useUpdatePatternTemplate(id: string) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (command: PatternTemplateUpdateCommand) =>
      updatePatternTemplate(id, command),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["pattern-templates"] });
      queryClient.invalidateQueries({ queryKey: ["pattern-templates", id] });
    },
  });
}
