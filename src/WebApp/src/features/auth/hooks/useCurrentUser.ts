import { useQuery } from "@tanstack/react-query";
import { authApi } from "../api/authApi";

export const currentUserQueryKey = ["auth", "currentUser"] as const;

export function useCurrentUser() {
  return useQuery({
    queryKey: currentUserQueryKey,
    queryFn: async () => await authApi.fetchCurrentUser(),
    retry: false, // don't retry a failed auth check
    staleTime: Infinity, // user data doesn't go stale on its own — we invalidate manually on login/logout
  });
}
