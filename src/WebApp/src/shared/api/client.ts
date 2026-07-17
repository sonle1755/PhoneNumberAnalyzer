import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5024/api",
  headers: {
    "Content-Type": "application/json",
  },
});
