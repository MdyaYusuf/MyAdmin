import React from "react";
import { ToastProvider } from "./ToastProvider";

interface AppProviderProps {
  children: React.ReactNode;
}

export const AppProvider = ({ children }: AppProviderProps) => {
  return (
    <ToastProvider>
      {children}
    </ToastProvider>
  );
};