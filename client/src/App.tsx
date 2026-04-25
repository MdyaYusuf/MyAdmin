import { Button } from "@/core/components/ui/button";
import { apiClient } from "@/core/api/apiClient";

function App() {
  const testApi = async () => {
    await apiClient("/roles/test-delete", { method: "DELETE" });
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen gap-4">
      <h1 className="text-3xl font-bold tracking-tight">MyAdmin Dashboard</h1>
      <p className="text-muted-foreground">Screaming Architecture başarıyla kuruldu.</p>
      <Button onClick={testApi} variant="destructive">
        Jilet Gibi Test Et 🚀
      </Button>
    </div>
  );
}

export default App;