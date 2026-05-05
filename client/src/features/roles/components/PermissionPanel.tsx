const PermissionPanel = () => {
  return (
    <div className="lg:col-span-5 bg-surface-container-lowest rounded-xl p-6 shadow-sm border border-outline-variant/10 flex flex-col h-[600px]">
      <div className="border-b border-outline-variant/20 pb-4 mb-4">
        <div className="flex items-center justify-between mb-2">
          <h3 className="text-lg font-semibold text-on-surface">Super Administrator</h3>
          <span className="material-symbols-outlined text-primary text-[20px]">admin_panel_settings</span>
        </div>
        <p className="text-xs text-on-surface-variant">Syncing permissions for role.</p>
      </div>

      <div className="flex-1 overflow-y-auto pr-2 space-y-6">
        <div>
          <h4 className="text-sm font-semibold text-on-surface mb-3 flex items-center gap-2">
            <span className="material-symbols-outlined text-[16px]">group</span> User Management
          </h4>
          <div className="space-y-4 pl-6">
            {['users.create', 'users.edit', 'users.delete'].map((perm) => (
              <label key={perm} className="flex items-start gap-3 cursor-pointer group">
                <input type="checkbox" defaultChecked className="mt-0.5 rounded border-outline-variant/40 text-primary focus:ring-primary" />
                <div>
                  <div className="text-sm font-medium text-on-surface group-hover:text-primary transition-colors">{perm}</div>
                  <div className="text-xs text-on-surface-variant">Allow granular control over this resource.</div>
                </div>
              </label>
            ))}
          </div>
        </div>
      </div>

      <div className="pt-4 border-t border-outline-variant/20 mt-4 flex gap-3">
        <button className="flex-1 bg-gradient-to-r from-[#004ac6] to-[#2563eb] text-white rounded-md py-2 text-sm font-medium hover:brightness-110 transition-all">
          Sync Permissions
        </button>
        <button className="px-4 bg-gradient-to-r from-[#004ac6] to-[#2563eb] text-white rounded-md py-2 text-sm font-medium hover:bg-surface-container transition-all">
          Revert
        </button>
      </div>
    </div>
  );
};

export default PermissionPanel;