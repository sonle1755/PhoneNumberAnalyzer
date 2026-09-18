import {
  Button,
  Grid,
  List,
  ListItem,
  ListItemText,
  Stack,
  TextField,
  Typography,
  type SxProps,
  type Theme,
} from "@mui/material";
import { useState } from "react";
import { useAnalyzePhoneNumber } from "../hooks/useAnalyzePhoneNumber";

export function PhoneNumberAnalyzer({
  id,
  sectionSx,
}: {
  id: string;
  sectionSx: SxProps<Theme>;
}) {
  const maxLength = 500;
  const [value, setValue] = useState("");
  const characterCount = value.length;
  const itemSx: SxProps<Theme> = {
    mb: 1,
    border: 1,
    borderColor: "divider",
    bgcolor: "rgba(0,0,0,0.03)",
    ":hover": { bgcolor: "rgba(0,0,0,0.06)", borderColor: "primary.main" },
  };

  const { mutate, data, isPending } = useAnalyzePhoneNumber();

  function handleAnalyze() {
    if (value) mutate(value);
  }
  return (
    <Stack spacing={2} component="section" id={id} sx={sectionSx}>
      <Typography variant="subtitle1">// NHẬP VĂN BẢN BÊN DƯỚI</Typography>
      <Grid container columnSpacing={2} rowSpacing={4}>
        {/* Input  */}
        <Grid size={{ xs: 12, sm: 12, md: 6 }}>
          <Stack direction="row" sx={{ justifyContent: "space-between" }}>
            <Typography variant="subtitle1" sx={{ mb: 2 }}>
              VĂN BẢN ĐẦU VÀO
            </Typography>
            <Typography variant="subtitle1">
              {characterCount} / {maxLength} ký tự
            </Typography>
          </Stack>
          <TextField
            value={value}
            onChange={(e) => setValue(e.target.value)}
            multiline
            fullWidth
            variant="outlined"
            sx={{
              height: 320,
              alignItems: "flex-start",
              backgroundColor: "#f0ede7",
              borderRadius: 0,
              "& .MuiOutlinedInput-notchedOutline": {
                borderColor: "divider",
              },
              "& .MuiOutlinedInput-root": {
                height: "100%",
                alignItems: "flex-start",
                borderRadius: 0,
              },
            }}
            slotProps={{
              htmlInput: {
                maxLength,
              },
            }}
          />
        </Grid>

        <Grid
          size={12}
          sx={{ display: { xs: "block", sm: "block", md: "none" } }}
        >
          <Button variant="contained" disabled onClick={handleAnalyze}>
            PHÂN TÍCH VĂN BẢN →
          </Button>
        </Grid>
        {/* Results */}
        <Grid size={{ xs: 12, sm: 12, md: 6 }}>
          <Typography variant="subtitle1" sx={{ mb: 2 }}>
            KẾT QUẢ PHÁT HIỆN
          </Typography>
          <List
            component="nav"
            sx={{
              p: 2,
              overflowY: "auto",
              width: "100%",
              height: 320,
              backgroundColor: "#f0ede7",
              border: 1,
              borderColor: "divider",
            }}
          >
            {data &&
              data.map((item) => (
                <ListItem key={item.originalInput} sx={itemSx}>
                  <ListItemText primary={item.normalizedDigits} />
                </ListItem>
              ))}
          </List>
        </Grid>
        <Grid size={{ xs: 12, sm: 12, md: 6 }} sx={{ alignContent: "center" }}>
          <Button
            variant="contained"
            loading={isPending}
            onClick={handleAnalyze}
            sx={{
              display: {
                xs: "none",
                sm: "none",
                md: "block",
              },
            }}
          >
            PHÂN TÍCH VĂN BẢN →
          </Button>
        </Grid>
        <Grid
          size={{ xs: 12, sm: 12, md: 6 }}
          sx={{ alignContent: "center", textAlign: "right" }}
        >
          Tìm thấy:
        </Grid>
      </Grid>
    </Stack>
  );
}
