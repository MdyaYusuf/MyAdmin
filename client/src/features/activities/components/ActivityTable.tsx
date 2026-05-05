export const ActivityTable = () => (
  <div className="flex-1 border-r border-outline-variant/10 overflow-x-auto">
    <table className="w-full text-left text-sm whitespace-nowrap">
      <thead className="bg-surface-container-low text-on-surface-variant text-xs font-semibold uppercase tracking-wider border-b border-outline-variant/10">
        <tr>
          <th className="px-6 py-4">Timestamp</th>
          <th className="px-6 py-4">Actor</th>
          <th className="px-6 py-4">Action</th>
          <th className="px-6 py-4">Status</th>
        </tr>
      </thead>
      <tbody className="divide-y divide-outline-variant/10">
        <tr className="hover:bg-surface-container-highest transition-colors cursor-pointer bg-surface-container-high/30">
          <td className="px-6 py-4 text-on-surface-variant">2026-05-06 14:32:01</td>
          <td className="px-6 py-4 font-medium text-on-surface">admin@editorial.co</td>
          <td className="px-6 py-4"><span className="bg-primary-container/20 text-primary px-2 py-1 rounded-md text-xs font-semibold">Updated</span></td>
          <td className="px-6 py-4"><span className="material-symbols-outlined text-green-600">check_circle</span></td>
        </tr>
      </tbody>
    </table>
  </div>
);