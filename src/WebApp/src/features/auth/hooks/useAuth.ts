import { useCurrentUser } from "./useCurrentUser";
import { useLogin } from "./useLogin";
import { useLogout } from "./useLogout";

export function useAuth() {
  const { data: user, isLoading, isError } = useCurrentUser();
  const loginMutation = useLogin();
  const logoutMutation = useLogout();

  return {
    user: user ?? null,
    isLoading,
    isAuthenticated: !!user && !isError,
    login: (username: string, password: string) =>
      loginMutation.mutateAsync({ username, password }),
    logout: () => logoutMutation.mutateAsync(),
    isLoggingIn: loginMutation.isPending,
    loginError: loginMutation.error,
  };
}
