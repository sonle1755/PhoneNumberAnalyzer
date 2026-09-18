import { Grid, Link } from "@mui/material";
import { Logo } from "./Logo";

export function Footer() {
  return (
    <Grid
      component="footer"
      container
      sx={{
        alignItems: "center",
        justifyContent: "space-between",
        borderTop: 1,
        pl: 4,
        pr: 4,
        pt: 2,
        pb: 2,
        color: "rgba(0,0,0,0.5)",
        height: "80px",
      }}
    >
      <Grid size={4}>
        <Logo />
      </Grid>
      <Grid size={4} sx={{ textAlign: "center" }}>
        © 2026 · PNA · CÔNG CỤ PHÂN TÍCH MẪU SỐ ĐIỆN THOẠI
      </Grid>
      <Grid
        container
        size={4}
        columnSpacing={4}
        rowSpacing={0.2}
        sx={{
          alignItems: "flex-end",
          justifyContent: "flex-end",
          flexDirection: {
            xs: "column",
            md: "row",
          },
        }}
      >
        <Link href="" color="inherit" underline="hover">
          BẢO MẬT
        </Link>
        <Link href="" color="inherit" underline="hover">
          ĐIỀU KHOẢN
        </Link>
        <Link href="" color="inherit" underline="hover">
          TRẠNG THÁI
        </Link>
      </Grid>
    </Grid>
  );
}
