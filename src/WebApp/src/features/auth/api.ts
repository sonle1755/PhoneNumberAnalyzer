import { apiClient } from "../../shared/api/client";

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  username: string;
  avatarUrl: string | null;
  email: string | null;
  password: string;
}

export const login = async (data: LoginRequest) => {
  const res = await apiClient.post("/auth/login", data);
  return res.data;
};

export const register = async (data: RegisterRequest) => {
  const res = await apiClient.post("/auth/register", data);
  return res.data;
};
