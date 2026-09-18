import type { Theme } from "@emotion/react";
import { Button, Grid, Typography, type SxProps } from "@mui/material";

export function CallToActionBanner({
  sectionSx,
  handleRegister,
  handleGoToAnalyze,
}: {
  sectionSx: SxProps<Theme>;
  handleRegister: () => void;
  handleGoToAnalyze: () => void;
}) {
  const containerSx: SxProps<Theme> = {
    ...sectionSx,
    border: 1,
    borderColor: "divider",
    backgroundColor: "#2d40700f",
  };
  return (
    <Grid component="section" container sx={containerSx}>
      <Grid size={8}>
        <Typography variant="h6">TÍCH HỢP TRONG VÀI PHÚT</Typography>
        <Typography variant="h4" sx={{ fontWeight: 700 }}>
          Xây dựng cho quy trình.
        </Typography>
        <Typography variant="h4" sx={{ fontWeight: 700 }}>
          Sẵn sàng cho sản xuất.
        </Typography>
      </Grid>
      <Grid size={2}></Grid>
      <Grid container size={2} rowSpacing={2}>
        <Button variant="contained" fullWidth onClick={handleRegister}>
          ĐĂNG KÝ NGAY
        </Button>
        <Button variant="outlined" fullWidth onClick={handleGoToAnalyze}>
          DÙNG MIỄN PHÍ
        </Button>
      </Grid>
    </Grid>
  );
}
