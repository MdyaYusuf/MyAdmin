import { Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from '@/features/auth/pages/LoginPage';
import RegisterPage from '@/features/auth/pages/RegisterPage';
import ProtectedRoute from './ProtectedRoute';
import LandingPage from '../features/landing/pages/LandingPage';
import DashboardPage from '@/features/dashboard/pages/DashboardPage';
import { DashboardLayout } from '@/layouts/DashboardLayout';

export const AppRouter = () => {
  return (
    <Routes>
      <Route path="/" element={<LandingPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />

      <Route
        element={
          <ProtectedRoute>
            <DashboardLayout />
          </ProtectedRoute>
        }
      >
        <Route path="/dashboard" element={<DashboardPage />} />

        <Route path="/analytics" element={<div className="p-10">Analytics Content</div>} />
        <Route path="/settings" element={<div className="p-10">Settings Content</div>} />
      </Route>

      <Route
        path="/admin"
        element={
          <ProtectedRoute requiredRole="Admin">
            <DashboardLayout />
          </ProtectedRoute>
        }
      >
        <Route index element={<div className="p-10">Admin Dashboard - High Security Zone</div>} />
      </Route>

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
};