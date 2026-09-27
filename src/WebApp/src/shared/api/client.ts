import axios from "axios";
import type { CreateClientConfig } from "@/client/client.gen";

const instance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

export const createClientConfig: CreateClientConfig = (config) => ({
  ...config,
  axios: instance,
});
