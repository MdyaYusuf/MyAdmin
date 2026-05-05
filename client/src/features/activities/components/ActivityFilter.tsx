export const ActivityFilter = () => (
  <div className="bg-surface-container-lowest rounded-xl p-4 flex flex-wrap gap-4 items-center border border-outline-variant/10">
    <div className="flex-1 min-w-[200px]">
      <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1 block">Entity</label>
      <select className="w-full bg-surface border border-outline-variant/20 rounded-lg py-2 px-3 text-sm focus:border-primary text-on-surface appearance-none outline-none">
        <option>All Entities</option>
        <option>User</option>
        <option>Role</option>
      </select>
    </div>
    <div className="flex-1 min-w-[150px]">
      <label className="text-xs font-semibold text-on-surface-variant uppercase tracking-wider mb-1 block">Status</label>
      <div className="flex gap-2 h-[38px]">
        <button className="flex-1 bg-primary-container text-primary rounded-lg text-sm font-medium">All</button>
        <button className="flex-1 bg-gradient-to-r from-[#004ac6] to-[#2563eb] text-white rounded-lg text-sm font-medium hover:bg-surface-container-highest transition-colors">Success</button>
      </div>
    </div>
    <div className="flex-none pt-5">
      <button className="bg-gradient-to-r from-[#004ac6] to-[#2563eb] text-white px-6 py-2 rounded-lg text-sm font-medium hover:brightness-110 transition-all flex items-center gap-2 h-[38px]">
        <span className="material-symbols-outlined text-sm">filter_list</span> Filter
      </button>
    </div>
  </div>
);