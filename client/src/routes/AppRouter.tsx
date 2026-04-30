import { Routes, Route } from 'react-router-dom';
import LoginPage from '@/features/auth/pages/LoginPage';
import RegisterPage from '@/features/auth/pages/RegisterPage';
import ProtectedRoute from './ProtectedRoute';
// Dashboard bileşenini içe aktar

export const AppRouter = () => {
  return (
    <Routes>
      {/* Herkese Açık Rotalar */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />

      {/* Korumalı Rotalar */}
      <Route
        path="/dashboard"
        element={
          <ProtectedRoute>
            <div className="p-10 text-on-surface">Dashboard Area - Only for Authenticated Users</div>
          </ProtectedRoute>
        }
      />

      {/* Rol Bazlı Korumalı Rotalar (Örnek) */}
      <Route
        path="/admin"
        element={
          <ProtectedRoute requiredRole="Admin">
            <div className="p-10">Admin Panel - High Security Zone</div>
          </ProtectedRoute>
        }
      />
    </Routes>
  );
};