export const Sidebar = () => {
  const navItems = [
    { name: 'Dashboard', icon: 'dashboard', active: true },
    { name: 'Analytics', icon: 'analytics' },
    { name: 'Inventory', icon: 'inventory_2' },
    { name: 'Team', icon: 'group' },
    { name: 'Reports', icon: 'description' },
    { name: 'Settings', icon: 'settings' },
  ];

  return (
    <nav className="h-screen sticky left-0 top-0 w-64 bg-surface-container-low border-r border-outline-variant/10 flex flex-col py-6 px-4 hidden md:flex z-40">
      <div className="mb-8 px-4 flex items-center gap-3">
        <div className="w-10 h-10 rounded-lg bg-primary text-on-primary flex items-center justify-center font-bold text-xl">E</div>
        <div>
          <h1 className="text-xl font-black tracking-tighter text-on-surface">Editorial</h1>
          <p className="text-[10px] text-on-surface-variant font-medium uppercase tracking-widest">Command Center</p>
        </div>
      </div>

      <button className="mb-8 mx-4 bg-primary text-on-primary py-2.5 px-4 rounded-lg font-medium text-sm flex items-center justify-center gap-2 hover:brightness-110 transition-all shadow-sm">
        <span className="material-symbols-outlined text-[18px]">add</span>
        Create New
      </button>

      <div className="flex-1 flex flex-col gap-1 overflow-y-auto">
        {navItems.map((item) => (
          <a
            key={item.name}
            href="#"
            className={`px-4 py-2.5 rounded-lg flex items-center gap-3 text-sm font-medium transition-all ${item.active
                ? 'text-primary bg-surface-container-highest'
                : 'text-on-surface-variant hover:bg-surface-container-high hover:text-on-surface'
              }`}
          >
            <span className="material-symbols-outlined text-[20px]">{item.icon}</span>
            {item.name}
          </a>
        ))}
      </div>

      <div className="mt-auto pt-4 border-t border-outline-variant/10 flex flex-col gap-1">
        <a href="#" className="text-on-surface-variant hover:text-on-surface px-4 py-2 flex items-center gap-3 text-sm font-medium transition-colors">
          <span className="material-symbols-outlined text-[20px]">help</span> Support
        </a>
        <a href="#" className="text-on-surface-variant hover:text-on-surface px-4 py-2 flex items-center gap-3 text-sm font-medium transition-colors">
          <span className="material-symbols-outlined text-[20px]">logout</span> Sign Out
        </a>
      </div>
    </nav>
  );
};