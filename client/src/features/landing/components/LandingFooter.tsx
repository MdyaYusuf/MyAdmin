export const LandingFooter = () => {
  return (
    <footer className="bg-surface py-12 border-t border-outline-variant/20">
      <div className="max-w-7xl mx-auto px-6 flex flex-col md:flex-row justify-between items-center gap-6">
        <div className="font-bold text-on-surface">MyAdmin</div>
        <div className="flex gap-6 text-sm text-on-surface-variant">
          {["Privacy", "Terms", "Security", "Contact"].map(link => (
            <a key={link} href="#" className="hover:text-primary transition-colors">{link}</a>
          ))}
        </div>
        <div className="text-xs text-on-surface-variant">
          © 2026 MyAdmin. All rights reserved.
        </div>
      </div>
    </footer>
  );
};