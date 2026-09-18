import {
  Outlet,
  Link as RouterLink,
  useLocation,
  useNavigation,
} from "react-router-dom";
import { AnimatePresence, motion } from "framer-motion";
import {
  AppBar,
  Toolbar,
  Button,
  Box,
  Grid,
  IconButton,
  Link,
  type SxProps,
  type Theme,
  Drawer,
  Divider,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Stack,
} from "@mui/material";
import { useState } from "react";
import { Footer } from "./Footer";
import PersonAddAltRoundedIcon from "@mui/icons-material/PersonAddAltRounded";
import LoginIcon from "@mui/icons-material/Login";
import MenuIcon from "@mui/icons-material/Menu";
import { Logo } from "./Logo";
import { ScrollToTop } from "@/shared/components/ScrollToTop";
import { InitialTransition } from "@/shared/components/InitialTransition";

export function AppLayout({ window }: { window?: () => Window }) {
  const location = useLocation();
  const navigation = useNavigation();
  const isLoading = navigation.state === "loading";
  const container =
    window !== undefined ? () => window().document.body : undefined;
  const [mobileOpen, setMobileOpen] = useState(false);
  const handleDrawerToggle = () => {
    setMobileOpen((prevState) => !prevState);
  };

  const topbarItemSx: SxProps<Theme> = {
    textDecoration: "none",
    color: "text.primary",
    position: "relative",
    transition: 0.4,
    "::before": {
      content: '""',
      position: "absolute",
      width: 0,
      height: 4,
      bottom: -10,
      left: "50%",
      bgcolor: "#b9b5b1",
      transition: "all 0.4s",
    },
    ":hover::before": {
      width: "100%",
      left: 0,
    },
  };

  const drawer = (
    <Box onClick={handleDrawerToggle} sx={{ textAlign: "center" }}>
      <Stack sx={{ m: 2 }}>
        <Logo />
      </Stack>
      <Divider />
      <List>
        <ListItem key="analyze" disablePadding>
          <ListItemButton sx={{ textAlign: "center" }}>
            <ListItemText primary="PHÂN TÍCH" />
          </ListItemButton>
        </ListItem>
        <ListItem key="features" disablePadding>
          <ListItemButton sx={{ textAlign: "center" }}>
            <ListItemText primary="TÍNH NĂNG" />
          </ListItemButton>
        </ListItem>
        <ListItem key="how" disablePadding>
          <ListItemButton sx={{ textAlign: "center" }}>
            <ListItemText primary="CÁCH DÙNG" />
          </ListItemButton>
        </ListItem>
      </List>
    </Box>
  );
  return (
    <AnimatePresence mode="wait">
      <motion.section exit={{ opacity: 0 }}>
        <InitialTransition />
        <AppBar
          position="static"
          sx={{
            borderBottom: 1,
            borderColor: "divider",
            backgroundColor: "background.default",
          }}
        >
          <Toolbar sx={{ display: "flex" }}>
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{ mr: 2, display: { sm: "none" } }}
            >
              <MenuIcon />
            </IconButton>
            <Grid sx={{ flexGrow: 1 }}>
              <Logo />
            </Grid>

            <Box
              sx={{
                flexGrow: 1,
                cursor: "pointer",
                display: {
                  xs: "none",
                  sm: "none",
                  md: "flex",
                },
                gap: 4,
              }}
            >
              <Link component={RouterLink} to="/login" sx={topbarItemSx}>
                PHÂN TÍCH
              </Link>
              <Link component={RouterLink} to="/login" sx={topbarItemSx}>
                TÍNH NĂNG
              </Link>
              <Link component={RouterLink} to="/login" sx={topbarItemSx}>
                CÁCH DÙNG
              </Link>
            </Box>
            <Box sx={{ display: "flex", gap: 1 }}>
              <IconButton
                component={RouterLink}
                to="/login"
                sx={{
                  display: {
                    sm: "block",
                    md: "none",
                  },
                }}
              >
                <LoginIcon />
              </IconButton>
              <IconButton
                component={RouterLink}
                to="/register"
                color="primary"
                sx={{
                  display: {
                    sm: "block",
                    md: "none",
                  },
                }}
              >
                <PersonAddAltRoundedIcon />
              </IconButton>
              <Button
                component={RouterLink}
                to="/login"
                variant="outlined"
                sx={{
                  display: {
                    xs: "none",
                    sm: "none",
                    md: "block",
                  },
                }}
              >
                ĐĂNG NHẬP
              </Button>
              <Button
                component={RouterLink}
                to="/register"
                variant="contained"
                sx={{
                  display: {
                    xs: "none",
                    sm: "none",
                    md: "block",
                  },
                }}
              >
                ĐĂNG KÝ
              </Button>
            </Box>
          </Toolbar>
        </AppBar>
        <nav>
          <Drawer
            container={container}
            variant="temporary"
            open={mobileOpen}
            onClose={handleDrawerToggle}
            ModalProps={{
              keepMounted: true, // Better open performance on mobile.
            }}
            sx={{
              display: { xs: "block", sm: "none" },
              "& .MuiDrawer-paper": {
                boxSizing: "border-box",
                width: 240,
              },
            }}
          >
            {drawer}
          </Drawer>
        </nav>
        <Box component="main">
          <ScrollToTop />
          <Outlet />
        </Box>
        <Footer />
      </motion.section>
    </AnimatePresence>
  );
}
