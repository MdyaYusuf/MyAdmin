export const ActivityTable = () => {
  const activities = [
    { who: 'Sarah Chen', role: 'Editor in Chief', what: 'Published article "Q3 Market Analysis"', when: '10 mins ago', status: 'Success' },
    { who: 'Marcus Johnson', role: 'System Admin', what: 'Updated role permissions', when: '45 mins ago', status: 'Success' },
    { who: 'System API', role: 'Automated Task', what: 'Data synchronization with CRM', when: '2 hours ago', status: 'Failed' },
  ];

  return (
    <div className="bg-surface-container-lowest rounded-xl border border-outline-variant/10 flex flex-col h-[400px]">
      <div className="p-6 border-b border-outline-variant/5 flex justify-between items-center bg-surface-container-low/30 rounded-t-xl">
        <h3 className="font-headline font-bold text-on-surface">Recent Activities</h3>
        <button className="text-xs font-medium text-primary flex items-center gap-1">View All <span className="material-symbols-outlined text-[14px]">arrow_forward</span></button>
      </div>
      <div className="flex-1 overflow-y-auto">
        <table className="w-full text-left text-sm">
          <thead className="text-xs text-on-surface-variant font-medium sticky top-0 bg-white">
            <tr>
              <th className="px-6 py-4">Who</th>
              <th className="px-6 py-4">What</th>
              <th className="px-6 py-4">When</th>
              <th className="px-6 py-4">Status</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-outline-variant/5">
            {activities.map((act, i) => (
              <tr key={i} className="hover:bg-surface-container-low/40 transition-colors">
                <td className="px-6 py-3">
                  <div className="font-medium text-on-surface">{act.who}</div>
                  <div className="text-[11px] text-on-surface-variant">{act.role}</div>
                </td>
                <td className="px-6 py-3 text-on-surface-variant">{act.what}</td>
                <td className="px-6 py-3 text-on-surface-variant text-xs">{act.when}</td>
                <td className="px-6 py-3">
                  <span className={`px-2 py-1 rounded-full text-[10px] font-bold ${act.status === 'Success' ? 'bg-emerald-100 text-emerald-700' : 'bg-error-container text-on-error-container'}`}>
                    {act.status}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};