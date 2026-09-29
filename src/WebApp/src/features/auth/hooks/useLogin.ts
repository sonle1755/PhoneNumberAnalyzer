import { useMutation, useQueryClient } from "@tanstack/react-query";
import { authApi } from "../api/authApi";
import { currentUserQueryKey } from "./useCurrentUser";
import { setAccessToken } from "../accessTokenStore";

export function useLogin() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({
      username,
      password,
    }: {
      username: string;
      password: string;
    }) => {
      const data = await authApi.login({ username, password });
      setAccessToken(data.accessToken, data.expiresIn);
      return authApi.fetchCurrentUser();
    },
    onSuccess: (user) => {
      queryClient.setQueryData(currentUserQueryKey, user);
    },
  });
}
