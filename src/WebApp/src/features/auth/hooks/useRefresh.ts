import { useMutation } from "@tanstack/react-query";
import { authApi } from "../api/authApi";
import { setAccessToken } from "../accessTokenStore";

export function useRefresh() {
  return useMutation({
    mutationFn: async () => {
      const { accessToken, expiresIn } = await authApi.refresh();
      setAccessToken(accessToken, expiresIn);
    },
  });
}
