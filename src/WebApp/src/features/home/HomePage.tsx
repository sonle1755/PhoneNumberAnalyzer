import {
  Container,
  Typography,
  Button,
  Stack,
  Box,
  TextField,
} from "@mui/material";
import { Link as RouterLink } from "react-router-dom";

export default function HomePage() {
  return (
    <Container maxWidth="md" sx={{ mt: 10, textAlign: "center" }}>
      <Stack spacing={4}>
        <Box>
          <Typography variant="h3" fontWeight="bold" gutterBottom>
            Đánh giá số điện thoại
          </Typography>
        </Box>

        <TextField
          id="outlined-basic"
          label="Nhập số điện thoại cân đánh giá vào đây"
          variant="outlined"
        />
      </Stack>
    </Container>
  );
}
