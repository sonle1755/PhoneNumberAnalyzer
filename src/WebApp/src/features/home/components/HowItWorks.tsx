import { Box, Grid, Typography, type SxProps, type Theme } from "@mui/material";

export function HowItWorks({ sectionSx }: { sectionSx: SxProps<Theme> }) {
  const stepIndexSx: SxProps<Theme> = {
    fontWeight: 800,
    color: "rgba(0,0,0,0.07)",
  };
  const stepBoxSx: SxProps<Theme> = {
    borderLeft: "solid 1px rgba(0, 0, 0, 0.08)",
    pl: 2,
    display: "grid",
    gridTemplateRows: "auto auto 1fr",
    rowGap: 1,
  };
  return (
    <Box component="section" sx={sectionSx}>
      <Typography variant="subtitle1" sx={{ pb: 5 }}>
        // CÁCH HOẠT ĐỘNG
      </Typography>
      <Grid container>
        <Grid size={3} sx={stepBoxSx}>
          <Typography variant="h4" sx={stepIndexSx}>
            01
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            DÁN VĂN BẢN
          </Typography>
          <Typography variant="subtitle1">
            Dán bất kỳ tài liệu thô nào — email, hợp đồng, nhật ký hỗ trợ, CSV
            hoặc HTML đã cào.
          </Typography>
        </Grid>
        <Grid size={3} sx={stepBoxSx}>
          <Typography variant="h4" sx={stepIndexSx}>
            02
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            PHÂN TÍCH
          </Typography>
          <Typography variant="subtitle1">
            Công cụ regex quét đồng thời các định dạng NANP, E.164, nội địa và
            số khẩn cấp.
          </Typography>
        </Grid>
        <Grid size={3} sx={stepBoxSx}>
          <Typography variant="h4" sx={stepIndexSx}>
            03
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            PHÂN LOẠI
          </Typography>
          <Typography variant="subtitle1">
            Mỗi kết quả được gắn nhãn theo vùng, định dạng, tính hợp lệ và tiền
            tố nhà mạng.
          </Typography>
        </Grid>
        <Grid size={3} sx={stepBoxSx}>
          <Typography variant="h4" sx={stepIndexSx}>
            04
          </Typography>
          <Typography variant="h6" sx={{ fontWeight: 700 }}>
            XUẤT DỮ LIỆU
          </Typography>
          <Typography variant="subtitle1">
            Sao chép danh sách đã lọc trùng, tải JSON, hoặc đẩy trực tiếp vào
            CRM qua API.
          </Typography>
        </Grid>
      </Grid>
    </Box>
  );
}
