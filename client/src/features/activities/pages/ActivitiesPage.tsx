import { ActivityFilter } from '../components/ActivityFilter';
import { ActivityTable } from '../components/ActivityTable';
import { ActivityDetail } from '../components/ActivityDetail';

const ActivitiesPage = () => (
  <div className="flex-1 flex flex-col h-full overflow-hidden bg-surface-container-low">
    <main className="flex-1 overflow-y-auto p-8">
      <div className="max-w-7xl mx-auto space-y-6">
        <div className="flex justify-between items-end">
          <div>
            <h2 className="text-3xl font-bold text-on-background tracking-tight" style={{ letterSpacing: '-0.02em' }}>Activity Details</h2>
            <p className="text-sm text-on-surface-variant mt-1">System activity logs and data mutations.</p>
          </div>
          <button className="bg-surface-container-lowest text-on-surface border border-outline-variant/20 px-4 py-2 rounded-lg text-sm font-medium hover:bg-surface-container-highest transition-colors flex items-center gap-2">
            <span className="material-symbols-outlined text-sm">download</span> Export CSV
          </button>
        </div>
        <ActivityFilter />
        <div className="bg-surface-container-lowest rounded-xl overflow-hidden flex border border-outline-variant/10 shadow-sm">
          <ActivityTable />
          <ActivityDetail />
        </div>
      </div>
    </main>
  </div>
);

export default ActivitiesPage;