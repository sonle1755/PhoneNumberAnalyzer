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
import { routePaths } from "./routePaths";

export const router = createBrowserRouter([
  {
    element: <PublicLayout />,
    children: [
      {
        path: routePaths.home,
        element: <HomePage />,
      },
      {
        path: routePaths.login,
        element: <LoginPage />,
      },
      {
        path: routePaths.register,
        element: <RegisterPage />,
      },
    ],
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        path: routePaths.admin,
        element: <AppLayout />,
        children: [
          {
            index: true,
            element: <DashboardPage />,
          },
          {
            path: routePaths.patternTemplate,
            element: <ProtectedRoute />,
            children: [
              {
                index: true,
                element: <PatternTemplateListPage />,
              },
              {
                path: routePaths.patternTemplateCreate,
                element: <PatternTemplateCreatePage />,
              },
              {
                path: routePaths.patternTemplateEdit,
                element: <PatternTemplateEditPage />,
              },
            ],
          },
        ],
      },
    ],
  },
]);
