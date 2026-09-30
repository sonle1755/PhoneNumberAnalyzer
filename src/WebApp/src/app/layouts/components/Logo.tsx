import { Link as RouterLink } from "react-router-dom";
import { Link } from "@mui/material";
import { routePaths } from "@/app/routePaths";

export function Logo() {
  return (
    <Link
      component={RouterLink}
      to={routePaths.home}
      sx={{ fontWeight: 700 }}
      underline="none"
    >
      <span
        style={{
          color: "#1e3560",
        }}
      >
        SOISIM
      </span>
      <span style={{ color: "rgba(0,0,0,0.2)" }}>.VN</span>
    </Link>
  );
}
