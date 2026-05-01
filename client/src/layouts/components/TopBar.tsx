export const TopBar = () => {
  return (
    <header className="bg-white/80 backdrop-blur-md sticky top-0 w-full z-30 border-b border-outline-variant/10 flex items-center justify-between px-8 h-16">
      <span className="text-lg font-bold text-on-surface">Admin Console</span>

      <div className="flex-1 max-w-md mx-8 hidden lg:block">
        <div className="relative flex items-center focus-within:ring-2 focus-within:ring-primary/20 rounded-lg transition-all">
          <span className="material-symbols-outlined absolute left-3 text-on-surface-variant/50 text-[20px]">search</span>
          <input
            type="text"
            placeholder="Search..."
            className="w-full bg-surface-container-low border-none rounded-lg pl-10 pr-4 py-2 text-sm focus:bg-surface-container-lowest transition-colors"
          />
        </div>
      </div>

      <div className="flex items-center gap-4">
        <button className="text-on-surface-variant hover:text-primary transition-all relative">
          <span className="material-symbols-outlined">notifications</span>
          <span className="absolute top-0 right-0 w-2 h-2 bg-error rounded-full border-2 border-white"></span>
        </button>
        <div className="w-8 h-8 rounded-full overflow-hidden border border-outline-variant/20">
          <img src="https://lh3.googleusercontent.com/a/default-user" alt="Profile" className="w-full h-full object-cover" />
        </div>
      </div>
    </header>
  );
};