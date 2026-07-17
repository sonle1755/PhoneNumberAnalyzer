import { Routes, Route, Navigate } from "react-router-dom";
import HomePage from "./features/home/HomePage";
import LoginPage from "./features/auth/LoginPage";
import RegisterPage from "./features/auth/RegisterPage";
import UsersPage from "./features/users/UsersPage";
import Layout from "./app/Layout";

export default function App() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/users" element={<UsersPage />} />
      </Route>
    </Routes>
  );
}
