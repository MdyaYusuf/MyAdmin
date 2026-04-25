import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PATHS } from "./paths";
const LoginPage = () => <div>Giriş Sayfası (Auth Feature)</div>;
const DashboardPage = () => <div>Admin Paneli (Dashboard Feature)</div>;

export const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path={PATHS.LOGIN} element={<LoginPage />} />

        <Route path={PATHS.DASHBOARD} element={<DashboardPage />} />

        <Route path="*" element={<Navigate to={PATHS.LOGIN} replace />} />
      </Routes>
    </BrowserRouter>
  );
};