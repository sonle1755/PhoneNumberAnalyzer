import { GlobalStyles as MuiGlobalStyles } from "@mui/material";

export function GlobalStyles() {
  return (
    <MuiGlobalStyles
      styles={(theme) => ({
        html: {
          scrollbarGutter: "stable",
        },
        body: {
          margin: 0,
          minHeight: "100dvh",
        },
        "#root": {
          minHeight: "100dvh",
        },
        "*": {
          scrollbarWidth: "thin",
          scrollbarColor: `${theme.palette.grey[500]} transparent`,
        },
        "*::-webkit-scrollbar": {
          width: "8px",
          height: "8px",
        },

        "*::-webkit-scrollbar-track": {
          background: "transparent",
        },

        "*::-webkit-scrollbar-thumb": {
          backgroundColor: theme.palette.grey[500],
          borderRadius: "4px",
        },

        "*::-webkit-scrollbar-thumb:hover": {
          backgroundColor: theme.palette.grey[700],
        },
      })}
    />
  );
}
