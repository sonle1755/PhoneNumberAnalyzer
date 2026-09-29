import { defineConfig } from "@hey-api/openapi-ts";

export default defineConfig({
  input: "http://localhost:5174/swagger/v1/swagger.json",
  output: "src/client",
  plugins: [
    {
      name: "@hey-api/client-axios",
      runtimeConfigPath: "./src/shared/api/client.ts",
    },
    {
      name: "@hey-api/typescript",
      enums: "javascript",
    },
    "@tanstack/react-query",
  ],
});
