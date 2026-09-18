import { apiClient } from "../../../shared/api/client";
import type { LoginRequest, RegisterRequest } from "@/shared/api/Api";

export async function register(request: RegisterRequest): Promise<void> {
  await apiClient.api.authRegisterCreate(request);
}

export async function login(request: LoginRequest): Promise<void> {
  await apiClient.api.authLoginCreate(request);
}
