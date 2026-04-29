// src/app/AppRouter.tsx
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PATHS } from "./paths";
import LandingPage from "@/features/landing/pages/LandingPage"; // Landing sayfasını import et

// Placeholder'lar (Bunları zamanla gerçek feature sayfalarıyla değiştireceğiz)
const LoginPage = () => <div>Giriş Sayfası</div>;
const DashboardPage = () => <div>Admin Paneli</div>;

export const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* Ana Sayfa (Landing) - Artık kök dizinde bu açılacak */}
        <Route path={PATHS.HOME} element={<LandingPage />} />

        {/* Auth Rotaları */}
        <Route path={PATHS.LOGIN} element={<LoginPage />} />

        {/* Korumalı Rotalar (İleride PrivateRoute ile sarmalayacağız) */}
        <Route path={PATHS.DASHBOARD} element={<DashboardPage />} />

        {/* Tanımlanmayan yollar için ana sayfaya yönlendir */}
        <Route path="*" element={<Navigate to={PATHS.HOME} replace />} />
      </Routes>
    </BrowserRouter>
  );
};