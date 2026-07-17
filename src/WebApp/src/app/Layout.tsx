import { Outlet, Link as RouterLink } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Stack,
  Link,
} from "@mui/material";
import LoginIcon from "@mui/icons-material/Login";
import PersonAddIcon from "@mui/icons-material/PersonAdd";

export default function Layout() {
  return (
    <>
      <AppBar position="static">
        <Toolbar sx={{ display: "flex" }}>
          <Typography
            component={RouterLink}
            to="/"
            sx={{
              flexGrow: 1,
              textDecoration: "none",
              color: "inherit",
              fontWeight: 600,
              cursor: "pointer",
            }}
          >
            PNA
          </Typography>
          <Button
            component={RouterLink}
            to="/login"
            startIcon={<LoginIcon />}
            sx={{
              textTransform: "none",
              fontWeight: 600,
              borderRadius: 2,
              px: 2,
              color: "text.primary",
              "&:hover": {
                backgroundColor: "action.hover",
              },
            }}
          >
            Đăng nhập
          </Button>
          <Button
            component={RouterLink}
            to="/register"
            variant="contained"
            startIcon={<PersonAddIcon />}
            sx={{
              textTransform: "none",
              fontWeight: 600,
              borderRadius: 2,
              px: 2.5,
              boxShadow: "none",
              "&:hover": {
                backgroundColor: "action.hover",
              },
            }}
          >
            Đăng ký
          </Button>
        </Toolbar>
      </AppBar>

      <Outlet />
    </>
  );
}
