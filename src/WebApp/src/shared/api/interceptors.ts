// import { api } from "./client";
//
// api.interceptors.request.use((config) => {
//   const token = localStorage.getItem("token");
//
//   if (token) {
//     config.headers.Authorization = `Bearer ${token}`;
//   }
//
//   return config;
// });
//
// api.interceptors.response.use(
//   (res) => res,
//   (err) => {
//     if (err.response?.status === 401) {
//       // logout / redirect
//     }
//     return Promise.reject(err);
//   },
// );
