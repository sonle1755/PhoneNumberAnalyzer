import { useMutation, useQueryClient } from "@tanstack/react-query";
import { register } from "../api/authApi";

export function useRegister() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: register,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["register"] });
    },
  });
}
