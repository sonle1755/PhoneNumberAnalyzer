import { useMutation, useQueryClient } from "@tanstack/react-query";
import { authApi } from "../api/authApi";
import { setAccessToken } from "../accessTokenStore";
import { currentUserQueryKey } from "./useCurrentUser";

export function useLogout() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: authApi.logout,
    onSuccess: () => {
      setAccessToken(null, null);
      queryClient.setQueryData(currentUserQueryKey, null);
      queryClient.removeQueries({ queryKey: currentUserQueryKey }); // clear cache entirely
    },
  });
}
