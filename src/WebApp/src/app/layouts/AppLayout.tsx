import { useAuth } from "@/features/auth/hooks/useAuth";
import { ScrollToTop } from "@/shared/components/ScrollToTop";
import MuiDrawer from "@mui/material/Drawer";
import MuiAppBar, {
  type AppBarProps as MuiAppBarProps,
} from "@mui/material/AppBar";
import {
  Box,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Toolbar,
  type Theme,
  Container,
  ListItemIcon,
  type CSSObject,
  styled,
  Tooltip,
  Stack,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useState } from "react";
import { Outlet, Link as RouterLink } from "react-router-dom";
import { Logo } from "./components/Logo";
import { UserAvatar } from "./components/UserAvatar";
import SpaceDashboardRoundedIcon from "@mui/icons-material/SpaceDashboardRounded";
import PatternRoundedIcon from "@mui/icons-material/PatternRounded";
import NotificationsIconWithBadge from "./components/NotificationsIconWithBadge";

const drawerWidth = 180;

const openedMixin = (theme: Theme): CSSObject => ({
  width: drawerWidth,
  transition: theme.transitions.create("width", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen,
  }),
  boxShadow: "6px 0 8px -4px rgba(0, 0, 0, 0.25)",
  borderRadius: 6,
  overflowX: "auto",
  ".MuiListItemIcon-root": {
    marginRight: 3,
  },
  ".MuiListItemText-root": {
    opacity: 1,
  },
});

const closedMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create("width", {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  overflowX: "hidden",
  width: `calc(${theme.spacing(7)} + 1px)`,
  [theme.breakpoints.up("sm")]: {
    width: `calc(${theme.spacing(8)} + 1px)`,
  },
  "&:hover .MuiDrawer-paper": openedMixin(theme),
});

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== "open",
})<AppBarProps>(({ theme }) => ({
  zIndex: theme.zIndex.drawer + 1,
  variants: [
    {
      props: ({ open }) => open,
      style: {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(["width", "margin"], {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.enteringScreen,
        }),
      },
    },
  ],
}));

const Drawer = styled(MuiDrawer, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme }) => ({
  width: drawerWidth,
  flexShrink: 0,
  position: "relative",
  whiteSpace: "nowrap",
  boxSizing: "border-box",
  zIndex: theme.zIndex.drawer,
  ":hover": {
    overflow: "visible",
  },
  // ":hover .MuiListItemButton-root": {
  //   justifyContent: "initial",
  // },
  ":hover .MuiListItemIcon-root": {
    marginRight: 3,
  },
  ":hover .MuiListItemText-root": {
    opacity: 1,
  },
  // Paper is absolute inside the root (not fixed to the viewport),
  // so it starts at the parent's content box and can overflow the root on hover.
  "& .MuiDrawer-paper": {
    position: "absolute",
    backgroundColor: theme.palette.background.default,
    border: 0,
  },
  variants: [
    {
      props: ({ open }) => open,
      style: {
        ...openedMixin(theme),
        "& .MuiDrawer-paper": openedMixin(theme),
      },
    },
    {
      props: ({ open }) => !open,
      style: {
        ...closedMixin(theme),
        "& .MuiDrawer-paper": closedMixin(theme),
      },
    },
  ],
}));

export function AppLayout() {
  const { user } = useAuth();
  const [open, setOpen] = useState(false);

  return (
    <Container
      sx={{
        height: "100vh",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <AppBar
        elevation={0}
        position="relative"
        sx={{
          zIndex: (theme) => theme.zIndex.drawer + 1,
          bgcolor: "background.default",
        }}
      >
        <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
          <Box>
            <IconButton
              color="inherit"
              aria-label="open drawer"
              onClick={() => setOpen((prev) => !prev)}
              edge="start"
              sx={{ marginRight: 2 }}
            >
              <Tooltip
                title="Toggle Navigation"
                describeChild
                arrow
                placement="left-start"
                slotProps={{
                  tooltip: {
                    sx: {
                      fontSize: ".7rem",
                    },
                  },
                }}
              >
                <MenuIcon />
              </Tooltip>
            </IconButton>
            <Logo />
          </Box>
          <Stack direction="row" spacing={1} sx={{ alignItems: "center" }}>
            <NotificationsIconWithBadge />
            {user && <UserAvatar username={user.username} />}
          </Stack>
        </Toolbar>
      </AppBar>
      <Box sx={{ display: "flex", flex: 1, minHeight: 0 }}>
        <Drawer variant="permanent" open={open}>
          <List>
            <ListItem
              key="adminDashboard"
              disablePadding
              sx={{ display: "block" }}
            >
              <ListItemButton
                component={RouterLink}
                to="/admin"
                sx={{
                  minHeight: 48,
                  px: 2.5,
                  justifyContent: "center",
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    justifyContent: "center",
                    mr: "auto",
                  }}
                >
                  <SpaceDashboardRoundedIcon />
                </ListItemIcon>
                <ListItemText primary="Dashboard" sx={{ opacity: 0 }} />
              </ListItemButton>
              <ListItemButton
                component={RouterLink}
                to="/admin/pattern-templates"
                sx={{
                  minHeight: 48,
                  px: 2.5,
                  justifyContent: "center",
                }}
              >
                <ListItemIcon
                  sx={{
                    minWidth: 0,
                    justifyContent: "center",
                    mr: "auto",
                  }}
                >
                  <PatternRoundedIcon />
                </ListItemIcon>
                <ListItemText primary="Pattern Templates" sx={{ opacity: 0 }} />
              </ListItemButton>
            </ListItem>
          </List>
        </Drawer>
        <Box
          component="main"
          sx={{ flexGrow: 1, p: 3, minWidth: 0, overflow: "auto" }}
        >
          <ScrollToTop />
          <Outlet />
        </Box>
      </Box>
    </Container>
  );
}
