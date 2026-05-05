const RoleList = () => {
  const roles = [
    { id: 1, name: 'Super Administrator', badge: 'System', desc: 'Full access to all system features.', users: 42, perms: 156, active: true },
    { id: 2, name: 'Content Editor', desc: 'Can create, edit, and publish content.', users: 128, perms: 45 },
    { id: 3, name: 'Financial Analyst', badge: 'Restricted', desc: 'View-only access to financial reports.', users: 12, perms: 18, isError: true },
  ];

  return (
    <div className="lg:col-span-7 bg-surface-container-lowest rounded-xl p-6 shadow-sm border border-outline-variant/10">
      <div className="flex justify-between items-center mb-6">
        <h3 className="text-lg font-semibold text-on-surface">Active Roles</h3>
        <button className="text-sm font-medium text-primary hover:text-primary-container transition-colors flex items-center gap-1">
          <span className="material-symbols-outlined text-[18px]">add</span> New Role
        </button>
      </div>
      <div className="space-y-3">
        {roles.map((role) => (
          <div
            key={role.id}
            className={`p-4 rounded-lg cursor-pointer transition-colors flex items-center justify-between border ${role.active ? 'bg-surface-container-highest border-transparent' : 'bg-surface-container-low border-outline-variant/10 hover:bg-surface-container'
              }`}
          >
            <div>
              <div className="font-semibold text-on-surface flex items-center gap-2">
                {role.name}
                {role.badge && (
                  <span className={`text-[10px] px-2 py-0.5 rounded-full font-bold uppercase tracking-wider ${role.isError ? 'bg-error-container text-on-error-container' : 'bg-primary/10 text-primary'
                    }`}>
                    {role.badge}
                  </span>
                )}
              </div>
              <div className="text-xs text-on-surface-variant mt-1">{role.desc}</div>
            </div>
            <div className="text-right">
              <div className="text-sm font-medium text-on-surface">{role.users} Users</div>
              <div className="text-xs text-on-surface-variant">{role.perms} Permissions</div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RoleList;