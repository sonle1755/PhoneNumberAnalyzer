import {
  postApiAuthLogin,
  postApiAuthLogout,
  postApiAuthRefresh,
  postApiAuthRegister,
  getApiAuthMe,
} from "@/client";
import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
} from "@/client/types.gen";
export const authApi = {
  login: async (credentials: LoginRequest): Promise<LoginResponse> => {
    const { data, error } = await postApiAuthLogin({ body: credentials });

    if (error || !data) {
      throw new Error("loginfailed");
    }
    return data;
  },

  logout: () => postApiAuthLogout(),

  refresh: () => postApiAuthRefresh().then((r) => r.data),

  register: (request: RegisterRequest) =>
    postApiAuthRegister({ body: request }),

  fetchCurrentUser: async () => {
    const { data, error } = await getApiAuthMe();
    if (error || !data) {
      throw new Error("Not authenticated!");
    }
    return data;
  },
};
