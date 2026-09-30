import { createBrowserRouter } from "react-router-dom";

import HomePage from "@/features/home/HomePage";
import LoginPage from "@/features/auth/pages/LoginPage";
import RegisterPage from "@/features/auth/pages/RegisterPage";
import {
  PatternTemplateListPage,
  PatternTemplateCreatePage,
  PatternTemplateEditPage,
} from "@/features/pattern-templates";
import { PublicLayout } from "./layouts/PublicLayout";
import { AppLayout } from "./layouts/AppLayout";
import ProtectedRoute from "@/features/auth/components/ProtectedRoute";
import { DashboardPage } from "@/features/admin/pages/DashboardPage";

export const router = createBrowserRouter([
  {
    element: <PublicLayout />,
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
    ],
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        path: "/admin",
        element: <AppLayout />,
        children: [
          {
            index: true,
            element: <DashboardPage />,
          },
          {
            path: "pattern-templates",
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
    ],
  },
]);
