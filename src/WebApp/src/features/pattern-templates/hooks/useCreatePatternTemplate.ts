import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createPatternTemplate } from "../api/patternTemplateApi";

export function useCreatePatternTemplate() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createPatternTemplate,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["pattern-templates"] });
    },
  });
}
