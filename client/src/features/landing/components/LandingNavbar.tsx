import { Link } from "react-router-dom";
import { Button } from "@/core/components/ui/button"; 

export const LandingNavbar = () => {
  return (
    <nav className="fixed top-0 w-full z-50 bg-white/80 backdrop-blur-xl border-b border-outline-variant/10">
      <div className="flex justify-between items-center max-w-7xl mx-auto px-6 h-16">
        <div className="text-2xl font-black tracking-tighter text-on-surface">
          MyAdmin
        </div>

        <div className="hidden md:flex items-center gap-8">
          {["Product", "Solutions", "Enterprise", "Pricing"].map((item) => (
            <a key={item} href="#" className="text-on-surface-variant font-medium hover:text-primary transition-colors">
              {item}
            </a>
          ))}
        </div>

        <div className="flex items-center gap-4">
          <Link to="/login" className="hidden md:inline-flex text-on-surface-variant font-medium hover:text-primary transition-colors">
            Sign In
          </Link>
          <Button className="bg-primary text-white hover:brightness-110 font-semibold px-6">
            Get Started
          </Button>
        </div>
      </div>
    </nav>
  );
};