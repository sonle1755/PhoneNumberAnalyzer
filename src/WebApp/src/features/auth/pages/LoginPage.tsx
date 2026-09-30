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
import { useLogin } from "../hooks/useLogin";

export default function LoginPage() {
  const navigate = useNavigate();
  const login = useLogin();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [inputError, setInputError] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!username || !password) {
      setInputError("Tên đăng nhập hoặc mật khẩu không được để trống!");
    } else {
      try {
        await login.mutateAsync({ username, password });
        navigate("/admin");
      } catch {
        console.log("login error");
      }
    }
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
            <Typography variant="h5">Chào mừng trở lại</Typography>
            <Typography variant="body2" color="text.secondary">
              Đăng nhập vào tài khoản của bạn
            </Typography>
          </Box>

          <form onSubmit={handleSubmit}>
            <Stack spacing={2}>
              <TextField
                error={!!inputError}
                label="Tên đăng nhập hoặc Email"
                fullWidth
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                autoFocus
              />

              <TextField
                error={!!inputError}
                label="Mật khẩu"
                type="password"
                fullWidth
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />

              {inputError && (
                <Typography variant="subtitle1" color="error">
                  {inputError}
                </Typography>
              )}

              {/* {loginError && ( */}
              {/*   <Typography variant="caption" color="error"> */}
              {/*     <pre>{JSON.stringify(loginError, null, 2)}</pre> */}
              {/*   </Typography> */}
              {/* )} */}

              <Button
                type="submit"
                variant="contained"
                size="large"
                sx={{ mt: 1 }}
              // disabled={isLoggingIn}
              >
                Đăng nhập
              </Button>
            </Stack>
          </form>

          <Typography variant="body1">
            Chưa có tài khoản?{" "}
            <Button component={RouterLink} to="/register">
              Đăng ký ngay
            </Button>
          </Typography>
        </Stack>
      </Paper>
    </Container>
  );
}
