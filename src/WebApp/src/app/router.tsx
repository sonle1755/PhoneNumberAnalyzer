import { createBrowserRouter } from "react-router-dom";

import HomePage from "@/features/home/HomePage";
import LoginPage from "@/features/auth/LoginPage";
import RegisterPage from "@/features/auth/RegisterPage";
import {
  PatternTemplateListPage,
  PatternTemplateCreatePage,
  PatternTemplateEditPage,
} from "@/features/pattern-templates";
import { AppLayout } from "./Layouts/AppLayout";

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
