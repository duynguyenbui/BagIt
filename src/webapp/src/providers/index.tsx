"use client";

import { SessionProvider } from "next-auth/react";
import React, { PropsWithChildren } from "react";
import { ThemeProvider } from "@/providers/ThemeProvider";

export const Providers = ({ children }: PropsWithChildren) => {
  return (
    <SessionProvider>
      <ThemeProvider
        attribute="class"
        defaultTheme="system"
        enableSystem
        disableTransitionOnChange
      >
        {children}
      </ThemeProvider>
    </SessionProvider>
  );
};
