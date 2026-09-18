import { Grid, Typography, type SxProps, type Theme } from "@mui/material";

export function Features({ sectionSx }: { sectionSx: SxProps<Theme> }) {
  const featureCaptionSx: SxProps<Theme> = {
    border: 1,
    borderColor: "divider",
    pt: 0.5,
    pb: 0.5,
    pl: 1,
    pr: 1,
    justifySelf: "start",
    color: "#1e3560",
  };
  const featureBoxSx: SxProps<Theme> = {
    ":hover": {
      backgroundColor: "#f0ede7",
      color: "#1e3560",
    },
    borderRight: "solid 1px rgba(0, 0, 0, 0.08)",
    padding: 2,
    display: "grid",
    gridTemplateRows: "auto auto 1fr",
    rowGap: 2,
  };
  return (
    <Grid container component="section" sx={sectionSx}>
      <Typography variant="subtitle1" sx={{ pb: 5 }}>
        // KHẢ NĂNG
      </Typography>

      <Grid
        container
        size={12}
        sx={{ border: "solid 1px rgba(0, 0, 0, 0.08)" }}
      >
        <Grid
          container
          size={{ xs: 12, sm: 6, md: 3 }}
          rowSpacing={2}
          sx={featureBoxSx}
        >
          <Typography variant="subtitle2" sx={featureCaptionSx}>
            ĐỊNH DẠNG
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            12+ định dạng được nhận diện
          </Typography>
          <Typography variant="subtitle1">
            NANP, E.164, ITU-T, mã ngắn quốc gia, số khẩn cấp và các biến thể
            miễn phí.
          </Typography>
        </Grid>
        <Grid
          container
          size={{ xs: 12, sm: 6, md: 3 }}
          rowSpacing={2}
          sx={featureBoxSx}
        >
          <Typography variant="subtitle2" sx={featureCaptionSx}>
            VÙNG
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            190+ mã quốc gia được hỗ trợ
          </Typography>
          <Typography variant="subtitle1">
            Bao phủ đầy đủ mã quốc gia với thuộc tính vùng dựa trên CLDR và xử
            lý mã quay số.
          </Typography>
        </Grid>
        <Grid
          container
          size={{ xs: 12, sm: 6, md: 3 }}
          rowSpacing={2}
          sx={featureBoxSx}
        >
          <Typography variant="subtitle2" sx={featureCaptionSx}>
            ĐỘ CHÍNH XÁC
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            Khớp an toàn không trùng lặp
          </Typography>
          <Typography variant="subtitle1">
            Bước loại trùng lặp ngăn đếm hai lần các số khớp với nhiều mẫu cùng
            lúc.
          </Typography>
        </Grid>
        <Grid
          container
          size={{ xs: 12, sm: 6, md: 3 }}
          rowSpacing={2}
          sx={featureBoxSx}
        >
          <Typography variant="subtitle2" sx={featureCaptionSx}>
            TỐC ĐỘ
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            &lt; 5ms thời gian phân tích
          </Typography>
          <Typography variant="subtitle1">
            Công cụ regex thuần túy, không gọi mạng. Hoạt động ngoại tuyến, xử
            lý văn bản 500k+ ký tự.
          </Typography>
        </Grid>
      </Grid>
    </Grid>
  );
}
