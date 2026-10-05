import { useState } from "react";
import Avatar from "@mui/material/Avatar";
import { IconButton, Menu, MenuItem } from "@mui/material";
import { useAuth } from "@/features/auth/hooks/useAuth";

function stringToColor(string: string) {
  let hash = 0;
  let i;

  for (i = 0; i < string.length; i += 1) {
    hash = string.charCodeAt(i) + ((hash << 5) - hash);
  }

  let color = "#";

  for (i = 0; i < 3; i += 1) {
    const value = (hash >> (i * 8)) & 0xff;
    color += `00${value.toString(16)}`.slice(-2);
  }

  return color;
}

function stringAvatar(name: string) {
  return {
    sx: {
      bgcolor: stringToColor(name),
      cursor: "pointer",
      width: "100%",
      height: "100%",
    },
    children: name[0],
  };
}

export function UserAvatar() {
  const { user, logout } = useAuth();
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const open = Boolean(anchorEl);

  const handleOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  function handleGotoProfile() {
    setAnchorEl(null);
  }

  function handleGotoSettings() {
    setAnchorEl(null);
  }

  const handleLogout = async () => {
    await logout();
  };

  const handleClose = () => {
    setAnchorEl(null);
  };
  return (
    <>
      <IconButton
        onClick={handleOpen}
        size="small"
        aria-controls={open ? "user-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        sx={{ p: 0, width: "30px", height: "30px" }}
      >
        <Avatar {...stringAvatar(user.username)} />
      </IconButton>
      <Menu
        id="user-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        disableScrollLock
      >
        <MenuItem onClick={handleGotoProfile}>Hồ sơ</MenuItem>

        <MenuItem onClick={handleGotoSettings}> Cài đặt</MenuItem>

        <MenuItem onClick={handleLogout}>Đăng xuất</MenuItem>
      </Menu>
    </>
  );
}
