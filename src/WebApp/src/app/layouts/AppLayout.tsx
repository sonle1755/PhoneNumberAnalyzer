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
  Drawer,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useState } from "react";
import {
  NavLink,
  Outlet,
  Link as RouterLink,
  useLocation,
} from "react-router-dom";
import { Logo } from "./components/Logo";
import { UserAvatar } from "./components/UserAvatar";
import SpaceDashboardOutlinedIcon from "@mui/icons-material/SpaceDashboardOutlined";
import LayersOutlinedIcon from "@mui/icons-material/LayersOutlined";
import NotificationsIconWithBadge from "./components/NotificationsIconWithBadge";
import NavigateNextIcon from "@mui/icons-material/NavigateNext";
import { routePaths } from "../routePaths";

const navigationItems = [
  {
    label: "Dashboard",
    path: routePaths.admin,
    icon: <SpaceDashboardOutlinedIcon />,
  },
  {
    label: "Mẫu SĐT",
    path: routePaths.patternTemplate,
    icon: <LayersOutlinedIcon />,
  },
];

export function AppLayout() {
  const { user } = useAuth();
  const [open, setOpen] = useState(false);

  return (
    <Box
      sx={{
        height: "100vh",
        display: "grid",
        gridTemplateColumns: "200px 1fr",
      }}
    >
      <Box
        sx={{
          height: "100%",
          borderRight: "solid 1px",
          borderRightColor: "divider",
        }}
      >
        <List
          disablePadding
          sx={{
            display: "flex",
            flexDirection: "column",
            height: "100%",
            overflowX: "hidden",
            bgcolor: "rgba(0,0,0,0.06)",
          }}
        >
          <ListItem
            sx={{ p: 2, display: "grid", gridTemplateColumns: "1fr 30px" }}
          >
            <Logo fontSize={14} />
            <NotificationsIconWithBadge />
          </ListItem>

          {navigationItems.map((item) => (
            <NavLink
              key={item.path}
              to={item.path}
              style={{ textDecoration: "none", color: "inherit" }}
              end
            >
              {({ isActive }) => (
                <ListItem
                  key={item.path}
                  disablePadding
                  sx={{
                    ...(isActive && {
                      bgcolor: "background.paper",
                      "& .MuiListItemText-primary": {
                        fontWeight: 700,
                      },
                    }),
                  }}
                >
                  <ListItemButton>
                    <ListItemIcon>{item.icon}</ListItemIcon>
                    <ListItemText primary={item.label} />
                  </ListItemButton>
                </ListItem>
              )}
            </NavLink>
          ))}

          <ListItem
            key="user-info"
            disablePadding
            sx={{
              marginTop: "auto",
              borderTop: "solid 1px",
              borderTopColor: "divider",
            }}
          >
            <ListItemButton>
              <ListItemIcon>
                <UserAvatar />
              </ListItemIcon>
              <ListItemText primary={user.firstName + " " + user.lastName} />
            </ListItemButton>
          </ListItem>
        </List>
      </Box>
      <Box component="main">
        <ScrollToTop />
        <Outlet />
      </Box>
    </Box>
  );
}
