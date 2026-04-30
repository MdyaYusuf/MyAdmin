import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useSelector } from 'react-redux';
import type { RootState } from '@/core/store/store';
import { Loader2 } from 'lucide-react';

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRole?: string;
}

/**
 * ProtectedRoute: Sayfa bazlı erişim kontrolü sağlar.
 * Rol kontrolü, nesne dizisi içindeki 'name' alanı üzerinden yapılır.
 */
const ProtectedRoute = ({ children, requiredRole }: ProtectedRouteProps) => {
  const { isAuthenticated, loading, user } = useSelector((state: RootState) => state.auth);
  const location = useLocation();

  // 1. Durum Kontrolü: Token yenileme veya ilk yükleme sırasındaki bekleme ekranı
  if (loading) {
    return (
      <div className="h-screen w-screen flex items-center justify-center bg-surface">
        <Loader2 className="animate-spin text-primary w-10 h-10" />
      </div>
    );
  }

  // 2. Kimlik Kontrolü: Giriş yapılmamışsa kullanıcıyı login sayfasına yönlendir
  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // 3. Yetki (Rol) Kontrolü: 
  // user.roles içindeki nesnelerin 'name' özelliğini, istenen rol ile karşılaştırıyoruz.
  if (requiredRole && user?.roles) {
    const hasPermission = user.roles.some(role => role.name === requiredRole);

    if (!hasPermission) {
      // Kullanıcı giriş yapmış ama bu sayfa için yetkisi yok
      return <Navigate to="/unauthorized" replace />;
    }
  }

  // 4. Onay: Tüm kontrollerden geçerse sayfayı göster
  return <>{children}</>;
};

export default ProtectedRoute;