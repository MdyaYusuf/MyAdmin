import { LandingNavbar } from "../components/LandingNavbar";
import { Hero } from "../components/Hero";
import { BentoFeatures } from "../components/BentoFeatures";
import { LandingFooter } from "../components/LandingFooter";

export default function LandingPage() {
  return (
    <div className="bg-surface text-on-surface font-sans antialiased min-h-screen flex flex-col">
      <LandingNavbar />

      <main className="flex-grow pt-24 pb-20">

        <Hero />
        <BentoFeatures />

      </main>

      <LandingFooter />
    </div>
  );
}