import {
  Container,
  Paper,
  Typography,
  TextField,
  Button,
  Stack,
  Box,
} from "@mui/material";
import { useState } from "react";
import { useNavigate, Link as RouterLink } from "react-router-dom";
import { useRegister } from "./hooks/useRegister";
import type { RegisterRequest } from "@/shared/api/Api";

export default function RegisterPage() {
  const navigate = useNavigate();
  const { mutate, isPending, error } = useRegister();
  const [formData, setFormData] = useState<RegisterRequest>({
    firstName: "",
    lastName: "",
    username: "",
    password: "",
    avatarUrl: null,
    email: null,
  });
  const [confirmPassword, setConfirmPassword] = useState(null);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutate(formData, {
      onSuccess: () => {
        // localStorage.setItem("token", data.accessToken);<t_��>hlua require"cmp.utils.feedkeys".run(82)
        // ý
        navigate("/");
      },
    });
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 10 }}>
      <Paper
        elevation={3}
        sx={{
          p: 4,
          borderRadius: 3,
        }}
      >
        <Stack spacing={3}>
          <Box>
            <Typography variant="h5">Tạo tài khoản mới</Typography>
          </Box>

          <form onSubmit={handleSubmit}>
            <Stack spacing={2}>
              <TextField
                label="Họ"
                fullWidth
                value={formData.firstName}
                onChange={(e) =>
                  setFormData({ ...formData, firstName: e.target.value })
                }
              />
              <TextField
                label="Tên"
                fullWidth
                value={formData.lastName}
                onChange={(e) =>
                  setFormData({ ...formData, lastName: e.target.value })
                }
              />
              <TextField
                label="Tên tài khoản"
                fullWidth
                value={formData.username}
                onChange={(e) =>
                  setFormData({ ...formData, username: e.target.value })
                }
              />

              <TextField
                label="Email"
                type="email"
                fullWidth
                value={formData.email}
                onChange={(e) =>
                  setFormData({ ...formData, email: e.target.value })
                }
              />

              <TextField
                label="Mật khẩu"
                type="password"
                fullWidth
                value={formData.password}
                onChange={(e) =>
                  setFormData({ ...formData, password: e.target.value })
                }
              />

              <TextField
                label="Xác nhận mật khẩu"
                type="password"
                fullWidth
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />

              {error && (
                <Typography color="error" variant="body2">
                  {error}
                </Typography>
              )}

              <Button
                type="submit"
                variant="contained"
                size="large"
                disabled={
                  !formData.username || !formData.password || !confirmPassword
                }
              >
                Tạo tài khoản
              </Button>
            </Stack>
          </form>

          <Typography variant="body2">
            Đã có tài khoản?{" "}
            <Button component={RouterLink} to="/login">
              Đăng nhập ngay
            </Button>
          </Typography>
        </Stack>
      </Paper>
    </Container>
  );
}
