export const ActivityDetail = () => (
  <div className="w-96 bg-surface-bright flex flex-col hidden lg:flex border-l border-outline-variant/10">
    <div className="p-6 border-b border-outline-variant/10 flex justify-between items-center">
      <div>
        <h3 className="font-bold text-on-background">Log Detail</h3>
        <p className="text-xs text-on-surface-variant mt-0.5">ID: req_8f72h9a</p>
      </div>
      <button className="text-on-surface-variant hover:text-on-surface"><span className="material-symbols-outlined">close</span></button>
    </div>
    <div className="p-6 flex-1 overflow-y-auto space-y-6">
      <div className="bg-inverse-surface rounded-lg p-4 font-mono text-xs text-inverse-on-surface leading-relaxed">
        <pre>{`{\n  "status": "active",\n  "role": "admin"\n}`}</pre>
      </div>
    </div>
  </div>
);