import NotificationsOutlinedIcon from "@mui/icons-material/NotificationsOutlined";
import { styled } from "@mui/material/styles";
import Badge, { badgeClasses } from "@mui/material/Badge";
import { IconButton } from "@mui/material";

const NotificationsBadge = styled(Badge)`
  & .${badgeClasses.badge} {
    top: -6px;
    right: 6px;

    min-width: 0.8rem;
    width: 0.8rem;
    height: 0.8rem;
    padding: 0;

    background-color: #2a4f73;
    border: solid 0.125rem #dbd8d2;
    border-radius: 50%;
  }
`;
export default function IconButtonWithBadge() {
  return (
    <IconButton sx={{ p: 0, minWidth: 30, minHeight: 30 }}>
      <NotificationsOutlinedIcon />
      <NotificationsBadge badgeContent />
    </IconButton>
  );
}
