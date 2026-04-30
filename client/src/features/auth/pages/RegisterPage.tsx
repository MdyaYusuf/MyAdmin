import React, { useState } from 'react';
import { LayoutGrid, ArrowRight, User, Mail, Lock, Loader2 } from 'lucide-react';
import { Link } from 'react-router-dom';
import { useAuth } from '../hooks/useAuth'; // useAuth hook'unu içe aktar

const RegisterPage = () => {
  // 1. useAuth hook'undan register fonksiyonunu ve yüklenme durumunu alıyoruz
  const { register, isRegisterLoading } = useAuth();

  const [formData, setFormData] = useState({
    username: '',
    email: '',
    password: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { id, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [id]: value,
    }));
  };

  /**
   * handleSubmit: Modern 'SyntheticEvent' kullanarak form gönderimini yönetir.
   * React Query mutasyonunu tetikler.
   */
  const handleSubmit = (e: React.SyntheticEvent) => {
    e.preventDefault();

    // Backend'e gidecek olan 'RegisterUserRequest' paketini gönderiyoruz
    register(formData);
  };

  return (
    <div className="bg-surface text-on-surface min-h-screen w-full flex flex-col lg:flex-row antialiased font-body overflow-x-hidden">

      {/* SOL SÜTUN: Form Alanı */}
      <main className="w-full lg:w-[45%] xl:w-[40%] flex flex-col justify-center px-8 md:px-16 lg:px-24 bg-surface-container-lowest z-10 py-12 shrink-0">
        <div className="max-w-md w-full mx-auto">

          <div className="mb-12 flex-shrink-0">
            <div className="flex items-center gap-2 mb-2">
              <LayoutGrid className="text-primary fill-primary w-6 h-6" />
              <span className="font-display font-black tracking-tighter text-xl text-on-surface uppercase">Arch_Systems</span>
            </div>
          </div>

          <div className="mb-10 text-left">
            <h1 className="font-headline text-3xl font-bold tracking-tight text-on-surface mb-3" style={{ letterSpacing: '-0.02em' }}>
              Orchestrate Your Identity.
            </h1>
            <p className="text-base text-on-surface-variant leading-relaxed">
              Enforce granular governance with .NET Core Precision Architecture[cite: 4].
            </p>
          </div>

          <form className="space-y-5" onSubmit={handleSubmit}>
            {/* Username Input */}
            <div>
              <label className="block text-sm font-medium text-on-surface-variant mb-1.5" htmlFor="username">Username</label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-outline">
                  <User size={18} />
                </div>
                <input
                  id="username"
                  value={formData.username}
                  onChange={handleChange}
                  className="w-full bg-surface border border-outline-variant/20 rounded-md pl-10 pr-4 py-3 text-sm text-on-surface focus:outline-none focus:border-primary transition-all"
                  placeholder="admin_sys_01"
                  type="text"
                  required
                />
              </div>
            </div>

            {/* Email Input */}
            <div>
              <label className="block text-sm font-medium text-on-surface-variant mb-1.5" htmlFor="email">Work Email</label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-outline">
                  <Mail size={18} />
                </div>
                <input
                  id="email"
                  value={formData.email}
                  onChange={handleChange}
                  className="w-full bg-surface border border-outline-variant/20 rounded-md pl-10 pr-4 py-3 text-sm text-on-surface focus:outline-none focus:border-primary transition-all"
                  placeholder="security@enterprise.com"
                  type="email"
                  required
                />
              </div>
            </div>

            {/* Password Input */}
            <div>
              <label className="block text-sm font-medium text-on-surface-variant mb-1.5" htmlFor="password">Password</label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-outline">
                  <Lock size={18} />
                </div>
                <input
                  id="password"
                  value={formData.password}
                  onChange={handleChange}
                  className="w-full bg-surface border border-outline-variant/20 rounded-md pl-10 pr-4 py-3 text-sm text-on-surface focus:outline-none focus:border-primary transition-all"
                  placeholder="••••••••"
                  type="password"
                  required
                />
              </div>
            </div>

            <div className="pt-4">
              <button
                type="submit"
                disabled={isRegisterLoading}
                className="w-full bg-primary text-white font-bold text-sm rounded-md py-3.5 hover:brightness-110 transition-all shadow-sm flex justify-center items-center gap-2 active:scale-[0.98] disabled:opacity-70 disabled:cursor-not-allowed"
              >
                {/* Yüklenme durumunda spinner gösterilir */}
                {isRegisterLoading ? (
                  <Loader2 className="animate-spin" size={18} />
                ) : (
                  <>
                    Establish Access
                    <ArrowRight size={18} />
                  </>
                )}
              </button>
            </div>
          </form>

          <div className="mt-8 text-center">
            <p className="text-sm text-on-surface-variant">
              Already integrated?
              <Link to="/login" className="text-primary font-bold hover:underline ml-1">Sign In</Link>
            </p>
          </div>
        </div>
      </main>

      {/* SAĞ SÜTUN */}
      <aside className="hidden lg:block lg:w-[55%] xl:w-[60%] relative bg-surface-container min-h-full shrink">
        <div className="absolute inset-0 w-full h-full">
          <img
            alt="Precision Architecture"
            className="w-full h-full object-cover opacity-90"
            src="https://images.unsplash.com/photo-1517245386807-bb43f82c33c4?q=80&w=2070&auto=format&fit=crop"
          />
          <div className="absolute inset-0 bg-gradient-to-tr from-surface/80 via-surface/20 to-transparent"></div>
        </div>
      </aside>
    </div>
  );
};

export default RegisterPage;