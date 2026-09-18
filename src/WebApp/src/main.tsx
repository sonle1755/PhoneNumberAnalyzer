import React from "react";
import ReactDOM from "react-dom/client";

import { App } from "./App";
import { ThemeProvider, CssBaseline } from "@mui/material";
import { theme } from "./app/theme";

import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { GlobalStyles } from "./shared/components/GlobalStyles";

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <GlobalStyles />
      <QueryClientProvider client={queryClient}>
        <App />
      </QueryClientProvider>
    </ThemeProvider>
  </React.StrictMode>,
);
