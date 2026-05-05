import { NavLink } from 'react-router-dom';

export const Sidebar = () => {
  const navItems = [
    { name: 'Dashboard', icon: 'dashboard', path: '/dashboard' },
    { name: 'User Management', icon: 'group', path: '/team' },
    { name: 'Roles and Permissions', icon: 'shield_person', path: '/roles' },
    { name: 'Activities', icon: 'history', path: '/activities' },
  ];

  return (
    <nav className="h-screen sticky left-0 top-0 w-64 bg-surface-container-low border-r border-outline-variant/10 flex flex-col py-6 px-4 hidden md:flex z-40">
      <div className="mb-10 px-4 flex items-center gap-3">
        <div className="w-10 h-10 rounded-lg bg-gradient-to-r from-[#004ac6] to-[#2563eb] text-white flex items-center justify-center font-bold text-xl shadow-sm">
          M
        </div>
        <div>
          <h1 className="text-xl font-black tracking-tighter text-on-surface">MyAdmin</h1>
          <p className="text-[10px] text-on-surface-variant font-medium uppercase tracking-widest">Command Center</p>
        </div>
      </div>

      <div className="flex flex-col gap-2">
        {navItems.map((item) => (
          <NavLink
            key={item.name}
            to={item.path}
            className={({ isActive }) =>
              `px-4 py-2.5 rounded-lg flex items-center gap-3 text-sm font-medium transition-all ${isActive
                ? 'text-primary bg-surface-container-highest shadow-sm' 
                : 'text-on-surface-variant hover:bg-surface-container-high hover:text-on-surface'
              }`
            }
          >
            <span className="material-symbols-outlined text-[20px]">{item.icon}</span>
            {item.name}
          </NavLink>
        ))}
      </div>
    </nav>
  );
};