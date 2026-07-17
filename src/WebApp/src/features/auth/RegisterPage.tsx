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
import { useRegister } from "./hooks";

export default function RegisterPage() {
  const navigate = useNavigate();
  const { mutate, isPending, error } = useRegister();
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [avatarUrl, setAvatarUrl] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutate(
      { firstName, lastName, username, avatarUrl, email, password },
      {
        onSuccess: (data) => {
          // localStorage.setItem("token", data.accessToken);
          navigate("/");
        },
      },
    );
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
            <Typography variant="h5" fontWeight="bold">
              Tạo tài khoản mới
            </Typography>
          </Box>

          <form onSubmit={handleSubmit}>
            <Stack spacing={2}>
              <TextField
                label="Họ"
                fullWidth
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
              />
              <TextField
                label="Tên"
                fullWidth
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
              <TextField
                label="Tên tài khoản"
                fullWidth
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />

              <TextField
                label="Email"
                type="email"
                fullWidth
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />

              <TextField
                label="Mật khẩu"
                type="Mật khẩu"
                fullWidth
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />

              <TextField
                label="Xác nhận mật khẩu"
                type="Mật khẩu"
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
                disabled={!username || !password || !confirmPassword}
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
