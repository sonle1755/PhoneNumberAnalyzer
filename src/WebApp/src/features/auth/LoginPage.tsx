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
import { useLogin } from "./hooks";

export default function LoginPage() {
  const navigate = useNavigate();
  const { mutate, isPending, error } = useLogin();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    mutate(
      { username, password },
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
              Welcome back
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Login to your account
            </Typography>
          </Box>

          <form onSubmit={handleSubmit}>
            <Stack spacing={2}>
              <TextField
                error={!!error}
                label="Username or Email"
                fullWidth
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />

              <TextField
                error={!!error}
                label="Password"
                type="password"
                fullWidth
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />

              {error && (
                <Typography variant="caption" color="error">
                  {error?.response?.data?.detail}
                </Typography>
              )}

              <Button
                type="submit"
                variant="contained"
                size="large"
                sx={{ mt: 1 }}
              >
                Login
              </Button>
            </Stack>
          </form>

          <Typography variant="body2">
            Don’t have an account?{" "}
            <Button component={RouterLink} to="/register">
              Register
            </Button>
          </Typography>
        </Stack>
      </Paper>
    </Container>
  );
}
