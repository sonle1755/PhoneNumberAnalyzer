import {
  Button,
  Grid,
  Stack,
  Typography,
  useTheme,
  type GridProps,
  type SxProps,
  type Theme,
} from "@mui/material";
import styles from "./Hero.module.css";

export function Hero({
  sectionSx,
  handleGoToAnalyze,
}: {
  sectionSx: SxProps<Theme>;
  handleGoToAnalyze: () => void;
}) {
  const theme = useTheme();
  const statBoxSx: SxProps<Theme> = {
    borderRight: `1px solid ${theme.palette.divider}`,
    borderBottom: `1px solid ${theme.palette.divider}`,
    borderTop: {
      xs: `1px solid ${theme.palette.divider}`,
      sm: `1px solid ${theme.palette.divider}`,
      md: "none",
    },
    borderLeft: {
      sm: "none",
      md: `1px solid ${theme.palette.divider}`,
    },
    textAlign: {
      sm: "center",
      md: "left",
    },
    alignContent: "center",
    pl: {
      xs: 0,
      sm: 0,
      md: 2,
    },
    minWidth: "100px",
  };

  const firstStatBoxSx: SxProps<Theme> = {
    ...statBoxSx,
    borderTop: `1px solid ${theme.palette.divider}`,
    borderLeft: `1px solid ${theme.palette.divider}`,
  };
  const statBoxSize: GridProps["size"] = {
    sm: 3,
    md: 9,
  };
  return (
    <Grid component="section" container spacing={2} sx={sectionSx}>
      <Grid size={{ sm: 12, md: 10 }}>
        <Stack spacing={4}>
          <Typography
            variant="subtitle1"
            sx={{
              pl: 1,
              color: "primary.main",
              borderLeft: `solid 2px ${theme.palette.primary.main}`,
            }}
          >
            CÔNG CỤ PHÂN TÍCH MẪU SỐ ĐIỆN THOẠI · v1.0.0
          </Typography>
          <Stack>
            <Typography
              variant="h1"
              sx={{
                fontSize: "clamp(2.5rem, 5vw, 3.5rem)",
                color: "black",
                lineHeight: 1.05,
              }}
            >
              Tìm mọi <br />
              số điện thoại
              <br />
              <span className={`${styles.textBlue}  ${styles.cursorBlink}`}>
                trong bất kỳ văn bản nào
              </span>
            </Typography>
          </Stack>
          <Grid container>
            <Grid size={{ sm: 12, md: 6 }}>
              <Typography
                variant="h6"
                sx={{ fontWeight: 400, color: "text.secondary" }}
              >
                Dán văn bản thô — email, hợp đồng, nhật ký hỗ trợ, HTML đã cào.
                Dialect trích xuất, xác thực và phân loại mọi mẫu số điện thoại
                tìm thấy.
              </Typography>
            </Grid>
            <Grid size={{ sm: 0, md: 6 }}></Grid>
          </Grid>
        </Stack>
      </Grid>
      <Stack
        direction="row"
        spacing={2}
        sx={{ mt: 2, mb: 2, display: { md: "none", sm: "block" } }}
      >
        <Button variant="contained" onClick={handleGoToAnalyze}>
          THỬ NGAY →
        </Button>
        <Button variant="outlined">TẢI VĂN BẢN MẪU</Button>
      </Stack>
      <Grid
        container
        spacing={0}
        size={{ sm: 12, md: 2 }}
        sx={{
          justifyContent: "flex-end",
          display: { xs: "none", sm: "flex" },
        }}
      >
        <Grid size={statBoxSize} sx={firstStatBoxSx}>
          <Typography
            variant="h4"
            sx={{ fontWeight: 700 }}
            className={styles.textBlue}
          >
            12+
          </Typography>
          <Typography variant="body1">MẪU SỐ</Typography>
        </Grid>

        <Grid size={statBoxSize} sx={statBoxSx}>
          <Typography
            variant="h4"
            sx={{ fontWeight: 700 }}
            className={styles.textBlue}
          >
            190+
          </Typography>
          <Typography variant="body1">VÙNG</Typography>
        </Grid>
        <Grid size={statBoxSize} sx={statBoxSx}>
          <Typography
            variant="h4"
            sx={{ fontWeight: 700 }}
            className={styles.textBlue}
          >
            &gt;5ms
          </Typography>
          <Typography variant="body1">PHÂN TÍCH</Typography>
        </Grid>
        <Grid size={statBoxSize} sx={statBoxSx}>
          <Typography
            variant="h4"
            sx={{ fontWeight: 700 }}
            className={styles.textBlue}
          >
            99.3%
          </Typography>
          <Typography variant="body1">ĐỘ CHÍNH XÁC</Typography>
        </Grid>
      </Grid>
      <Stack
        direction="row"
        spacing={2}
        sx={{ mt: 5, display: { xs: "none", sm: "none", md: "block" } }}
      >
        <Button variant="contained" onClick={handleGoToAnalyze}>
          THỬ NGAY →
        </Button>
        <Button variant="outlined">TẢI VĂN BẢN MẪU</Button>
      </Stack>
    </Grid>
  );
}
