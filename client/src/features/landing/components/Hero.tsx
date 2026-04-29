import { Rocket } from "lucide-react";
import { Button } from "@/core/components/ui/button";
import DashboardPreview from "@/assets/dashboard-preview.png";

export const Hero = () => {
  return (
    <section className="max-w-7xl mx-auto px-6 py-20 lg:py-32 flex flex-col items-center text-center">
      <div className="inline-flex items-center gap-2 px-3 py-1.5 rounded-full bg-surface-container-high text-on-surface mb-8">
        <Rocket className="w-4 h-4 text-primary" />
        <span className="text-xs font-semibold tracking-wider uppercase">Built for High-Stakes Operations</span>
      </div>

      <h1 className="text-5xl md:text-7xl font-extrabold tracking-tighter leading-tight mb-6 max-w-4xl text-on-surface">
        Sovereign Control Over Your <span className="text-primary">.NET Ecosystem</span>
      </h1>

      <p className="text-lg md:text-xl text-on-surface-variant max-w-2xl mx-auto mb-10 leading-relaxed">
        A high-fidelity management engine built with Screaming Architecture. Orchestrate identity, enforce granular RBAC, and monitor unalterable audit trails with surgical precision.
      </p>

      <div className="flex flex-col sm:flex-row items-center gap-4">
        <Button className="bg-primary text-white h-12 px-8 text-base shadow-[0_8px_20px_rgba(0,74,198,0.2)] font-semibold transition-all hover:scale-105">
          Launch Console
        </Button>
        <Button variant="secondary" className="h-12 px-8 text-base bg-surface-container-high text-on-surface font-semibold hover:bg-surface-container-highest transition-colors">
          View Architecture
        </Button>
      </div>

      <div className="mt-20 w-full max-w-5xl relative rounded-xl bg-surface-container-low p-2 md:p-4 border border-outline-variant/10 overflow-hidden shadow-2xl">
        <div className="w-full aspect-[16/9] rounded-lg bg-slate-200 relative overflow-hidden">
          <img
            src={DashboardPreview}
            alt="Precision Dashboard Console"
            className="w-full h-auto rounded-lg object-cover"
          />

          <div className="absolute inset-x-4 bottom-4 md:inset-x-8 md:bottom-8 h-1/3 bg-white/80 backdrop-blur-xl rounded-xl flex items-end p-6 border border-white/20">
            <div className="flex justify-between items-end w-full text-left">
              <div>
                <p className="text-xs font-semibold text-on-surface-variant mb-1 uppercase tracking-wider">Audit Trail Integrity</p>
                <p className="text-4xl md:text-5xl font-bold text-on-surface">
                  99.99<span className="text-base font-normal ml-1">% Verified</span>
                </p>
              </div>
              <div className="flex gap-2 items-end h-full">
                {[40, 60, 30, 80, 100].map((h, i) => (
                  <div
                    key={i}
                    style={{ height: `${h}%` }}
                    className={`w-2 rounded-t-sm ${i === 4 ? 'bg-primary-container' : 'bg-primary'}`}
                  />
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};