import NotificationsIcon from "@mui/icons-material/Notifications";
import { styled } from "@mui/material/styles";
import Badge, { badgeClasses } from "@mui/material/Badge";
import { IconButton } from "@mui/material";

const NotificationsBadge = styled(Badge)`
  & .${badgeClasses.badge} {
    top: -12px;
    right: 10px;
    background-color: #2a4f73;
    min-width: 1rem;
    height: 1rem;
    border: solid 0.125rem #e9e6e0;
  }
`;

export default function IconButtonWithBadge() {
  return (
    <IconButton sx={{ p: 0, width: 30, heigth: 30 }}>
      <NotificationsIcon
        fontSize="large"
        sx={{ heigth: "100%", width: "100%" }}
      />
      <NotificationsBadge badgeContent color="primary" overlap="circular" />
    </IconButton>
  );
}
