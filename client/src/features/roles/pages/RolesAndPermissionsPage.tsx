import RoleList from '../components/RoleList';
import PermissionPanel from '../components/PermissionPanel';

const RolesAndPermissionsPage = () => {
  return (
    <div className="p-8 max-w-7xl mx-auto w-full">
      <div className="mb-8">
        <h2 className="text-3xl font-bold text-on-surface tracking-tight mb-2" style={{ letterSpacing: '-0.02em' }}>
          Roles and Permissions
        </h2>
        <p className="text-on-surface-variant text-sm">Manage user roles and granular permissions across the organization.</p>
      </div>

      <div className="flex gap-6 border-b border-outline-variant/20 mb-8">
        <button className="pb-3 border-b-2 border-primary text-primary font-semibold text-sm px-1">Roles</button>
        <button className="pb-3 border-b-2 border-transparent text-on-surface-variant font-medium text-sm px-1">Permissions</button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-12 gap-6">
        <RoleList />
        <PermissionPanel />
      </div>
    </div>
  );
};

export default RolesAndPermissionsPage;