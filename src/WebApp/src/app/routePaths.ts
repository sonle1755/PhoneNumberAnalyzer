export const routePaths = {
  // PUBLIC ROUTE PATHS
  home: "/",
  login: "/auth/login",
  register: "/auth/register",

  // PRIVATE ROUTE PATHS
  admin: "/admin",
  patternTemplate: "/admin/pattern-template",
  patternTemplateCreate: "/admin/pattern-template/new",
  patternTemplateEdit: "/admin/pattern-template/:patternTemplateId",
} as const;
