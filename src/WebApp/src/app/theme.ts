import { createTheme } from "@mui/material/styles";

export const theme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#1e3560",
      contrastText: "#2c3244",
    },
    background: {
      default: "#E9E6E0",
      paper: "#f7f7f6",
    },
    text: {
      primary: "#2c3244",
    },
    divider: "rgba(0, 0, 0, 0.08)",
  },
  typography: {
    fontFamily: ["JetBrains Mono", "monospace"].join(","),
    fontSize: 12,
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: "0",
          variants: [
            {
              props: { variant: "contained" },
              style: {
                backgroundColor: "#1e3560",
                color: "white",
                fontWeight: 400,
                fontSize: 14,
                boxShadow: "none",
                "&:hover": {
                  boxShadow: "none",
                  backgroundColor: "#162848",
                },
              },
            },
            {
              props: { variant: "outlined" },
              style: {
                border: "solid 1px rgba(0, 0, 0, 0.18)",
                color: "rgba(0, 0, 0, 0.65)",
                fontWeight: 400,
                fontSize: 14,
                "&:hover": {
                  color: "rgba(0, 0, 0, 0.75)",
                  border: "solid 1px rgba(0, 0, 0, 0.4)",
                },
              },
            },
            {
              props: { variant: "text" },
              style: {
                color: "#2c3244",
                fontWeight: 400,
                fontSize: 14,
              },
            },
          ],
        },
      },
    },
  },
});
