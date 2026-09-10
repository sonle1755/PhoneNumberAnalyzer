import { useMutation, useQueryClient } from "@tanstack/react-query";
import { analyzePhoneNumber } from "../api/phoneAnalyzerApi";

export function useAnalyzePhoneNumber() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: analyzePhoneNumber,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["analyze-phone-number"] });
    },
  });
}
