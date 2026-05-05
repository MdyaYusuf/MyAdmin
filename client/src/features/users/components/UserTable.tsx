import React from 'react';

/**
 * UserTable: Kullanıcı verilerini listeler. 
 * "No-Line" kuralına göre kenarlık yerine tonal farklar kullanılmıştır.
 */
const UserTable = () => {
  // Örnek veri seti (Mock Data)
  const users = [
    { id: 1, name: 'Sarah Jenkins', email: 'sarah.j@editorial.com', status: 'Active', role: 'Admin', lastActive: '2 mins ago', avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCXcnkd00JXeM2glTAQNZjte3zztEuG_HL2dcmP5whLsL_aI_KctJdm2QcVB30kwSWHuQbP2WF2tbwTHDPB7QjRopqEv7sQhxoGeHOhfaQhQA8jXYCLVmNBbhO8WoVtzh7Fu4NRfmfQTX0XhPH4DcJ1F988HBLHjm151k1a8xacu5HA19evjeYlQ9_9xp0lgwmBzh81nVN-B01jq8Qe1LUO6YXzmT71D6ba6lejXo4LTubqmXnaaRXLfpKcMuoT0lz7H010UaQ8HD8' },
    { id: 2, name: 'Michael Rodriguez', email: 'm.rodriguez@editorial.com', status: 'Active', role: 'Editor', lastActive: '1 hour ago', initials: 'MR' },
    { id: 3, name: 'David Chen', email: 'd.chen@editorial.com', status: 'Offline', role: 'Viewer', lastActive: 'Yesterday', avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCiD6pOwnNSuajNoOylr5_tWmMblSrHWkjW090N9NfBxEatOhI4CC_8-DnkuRmjwmQlRinrArwmogbjHitgHNl_khWf04w0fvYJOA3ZfiATZQSJC44pHNJD7SneGxIKA_T0w7dnFpGd37nuPBKRSwUE7ouDvsbHZDoirdia0do-DcLYaoO-lXvIHbBj8CJBo2hZXk1-H8CFoiCYNPahxMHJ6JuhATHWex-CzKGZpENu5XqVwykrkPsA2mPamD9o0nnzPjh5OAafkrk' },
  ];

  return (
    <div className="bg-surface-container-lowest rounded-xl shadow-sm border border-outline-variant/20 overflow-hidden">
      {/* Tablo Üst Araç Çubuğu */}
      <div className="p-4 border-b border-outline-variant/10 flex flex-col sm:flex-row gap-4 justify-between items-center bg-surface-container-low/30">
        <div className="relative w-full sm:w-64">
          <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-on-surface-variant text-[18px]">filter_list</span>
          <input
            type="text"
            placeholder="Filter users..."
            className="w-full pl-10 pr-3 py-1.5 bg-surface border border-outline-variant/20 rounded-md text-sm focus:outline-none focus:ring-1 focus:ring-primary transition-all"
          />
        </div>
        <div className="text-sm text-on-surface-variant">
          Showing <span className="font-medium text-on-background">1-3</span> of <span className="font-medium text-on-background">24</span> users
        </div>
      </div>

      {/* Veri Tablosu */}
      <div className="overflow-x-auto">
        <table className="w-full text-left text-sm whitespace-nowrap">
          <thead className="bg-surface-container-low/50 text-on-surface-variant font-medium">
            <tr>
              <th className="px-6 py-3 border-b border-outline-variant/10">User</th>
              <th className="px-6 py-3 border-b border-outline-variant/10">Status</th>
              <th className="px-6 py-3 border-b border-outline-variant/10">Role</th>
              <th className="px-6 py-3 border-b border-outline-variant/10 text-right">Actions</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-outline-variant/10">
            {users.map((user) => (
              <tr key={user.id} className="hover:bg-surface-container-low/30 transition-colors">
                <td className="px-6 py-4 flex items-center gap-3">
                  {user.avatar ? (
                    <img src={user.avatar} alt={user.name} className="w-9 h-9 rounded-full object-cover border border-outline-variant/10" />
                  ) : (
                    <div className="w-9 h-9 rounded-full bg-secondary-container text-on-secondary-container flex items-center justify-center font-medium">
                      {user.initials}
                    </div>
                  )}
                  <div>
                    <div className="font-medium text-on-background">{user.name}</div>
                    <div className="text-xs text-on-surface-variant">{user.email}</div>
                  </div>
                </td>
                <td className="px-6 py-4">
                  <span className={`px-2 py-0.5 rounded-full text-xs font-medium ${user.status === 'Active' ? 'bg-[#d5e3fc] text-[#00174b]' : 'bg-surface-dim text-on-surface-variant'
                    }`}>
                    {user.status}
                  </span>
                </td>
                <td className="px-6 py-4 text-on-surface-variant">{user.role}</td>
                <td className="px-6 py-4 text-right">
                  <button className="text-primary hover:text-primary-container font-medium text-sm px-2 py-1 rounded hover:bg-primary/5 transition-colors">
                    Edit
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default UserTable;