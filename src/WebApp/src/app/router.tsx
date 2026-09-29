import { createBrowserRouter } from "react-router-dom";

import HomePage from "@/features/home/HomePage";
import LoginPage from "@/features/auth/pages/LoginPage";
import RegisterPage from "@/features/auth/pages/RegisterPage";
import {
  PatternTemplateListPage,
  PatternTemplateCreatePage,
  PatternTemplateEditPage,
} from "@/features/pattern-templates";
import { AppLayout } from "./layouts/AppLayout";
import ProtectedRoute from "@/features/auth/components/ProtectedRoute";

export const router = createBrowserRouter([
  {
    element: <AppLayout />,
    children: [
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/login",
        element: <LoginPage />,
      },
      {
        path: "/register",
        element: <RegisterPage />,
      },
      {
        path: "/pattern-templates",
        element: <ProtectedRoute />,
        children: [
          {
            index: true,
            element: <PatternTemplateListPage />,
          },
          {
            path: "new",
            element: <PatternTemplateCreatePage />,
          },
          {
            path: ":patternTemplateId",
            element: <PatternTemplateEditPage />,
          },
        ],
      },
    ],
  },
]);
