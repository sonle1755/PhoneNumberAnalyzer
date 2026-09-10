import { Api } from "./Api";

export const apiClient = new Api({
  baseURL: import.meta.env.VITE_API_BASE_URL,
});
