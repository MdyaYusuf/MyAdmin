import { Sidebar } from '../../../layouts/components/Sidebar';
import { TopBar } from '../../../layouts/components/TopBar';
import { StatCard } from '../components/StatCard';
import { ActivityTable } from '../components/ActivityTable';
import { InboxSummary } from '../components/InboxSummary';

const DashboardPage = () => {
  return (
    <div className="flex min-h-screen bg-surface text-on-surface font-body antialiased">
      <Sidebar />

      <main className="flex-1 flex flex-col min-w-0">
        <TopBar />

        <div className="p-8 lg:p-12 max-w-7xl mx-auto w-full flex-1">
          {/* Header Section */}
          <div className="mb-10 flex flex-col md:flex-row md:items-end justify-between gap-4">
            <div>
              <h2 className="text-3xl font-headline font-bold tracking-tight mb-1">
                System Overview
              </h2>
              <p className="text-sm text-on-surface-variant">
                Real-time metrics and activity feed from the editorial platform.[cite: 1]
              </p>
            </div>
            <button className="bg-surface-container-lowest border border-outline-variant/20 px-4 py-2 rounded-lg text-sm font-medium hover:bg-surface-container-low transition-all flex items-center gap-2">
              <span className="material-symbols-outlined text-[18px]">download</span>
              Export
            </button>
          </div>

          {/* Bento Grid: Stats */}
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            <StatCard title="Total Users" value="14,208" trend="+12%" icon="group" color="primary" />
            <StatCard title="Active Roles" value="856" subText="Across 12 departments" icon="badge" color="secondary" />
            <StatCard title="Today's Notifications" value="1,204" badge="42 UNREAD" icon="notifications" isBright />
            <StatCard title="Recent Failed Activities" value="23" subText="Requires immediate review" icon="warning" color="error" />
          </div>

          {/* Main Content Area: Activities & Inbox */}
          <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
            <div className="lg:col-span-3">
              <ActivityTable />
            </div>
            <div className="lg:col-span-1">
              <InboxSummary />
            </div>
          </div>
        </div>
      </main>
    </div>
  );
};

export default DashboardPage;