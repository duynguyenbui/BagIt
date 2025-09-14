"use client";

import { signIn, signOut, useSession } from "next-auth/react";
import * as React from "react";
import { useEffect, useState, useRef } from "react";
import { Button } from "@/components/ui/button";
import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
} from "@/components/ui/navigation-menu";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { ModeToggle } from "../ModeToggle";
import UserMenu from "../UserMenu";
import { HamburgerIcon, LogInIcon, MenuIcon, MountainIcon } from "lucide-react";
import { AUTHJS_SESSION_STATUSES } from "@/constants";
import { useRouter } from "next/navigation";

export interface Navbar05NavItem {
  href?: string;
  label: string;
}
export interface Navbar05Props extends React.HTMLAttributes<HTMLElement> {
  logo?: React.ReactNode;
  navigationLinks?: Navbar05NavItem[];
}

const defaultNavigationLinks: Navbar05NavItem[] = [
  { href: "/", label: "Home" },
  { href: "/about", label: "About" },
];

const Navbar = React.forwardRef<HTMLElement, Navbar05Props>(
  ({ className, navigationLinks = defaultNavigationLinks, ...props }, ref) => {
    const [isMobile, setIsMobile] = useState(false);
    const containerRef = useRef<HTMLElement>(null);
    const { status, data } = useSession();
    const router = useRouter();

    useEffect(() => {
      const checkWidth = () => {
        if (containerRef.current) {
          const width = containerRef.current.offsetWidth;
          setIsMobile(width < 768);
        }
      };
      checkWidth();
      const resizeObserver = new ResizeObserver(checkWidth);
      if (containerRef.current) {
        resizeObserver.observe(containerRef.current);
      }
      return () => {
        resizeObserver.disconnect();
      };
    }, []);
    const combinedRef = React.useCallback(
      (node: HTMLElement | null) => {
        containerRef.current = node;
        if (typeof ref === "function") {
          ref(node);
        } else if (ref) {
          ref.current = node;
        }
      },
      [ref]
    );
    return (
      <header
        ref={combinedRef}
        className={cn(
          "sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60 px-4 md:px-6 [&_*]:no-underline",
          className
        )}
        {...props}
      >
        <div className="container mx-auto flex h-16 max-w-screen-2xl items-center justify-between gap-4">
          {/* Left side */}
          <div className="flex items-center gap-2">
            {/* Mobile menu trigger */}
            {isMobile && (
              <Popover>
                <PopoverTrigger asChild>
                  <Button
                    className="group h-9 w-9 hover:bg-accent hover:text-accent-foreground"
                    variant="ghost"
                    size="icon"
                  >
                    <MenuIcon />
                  </Button>
                </PopoverTrigger>
                <PopoverContent align="start" className="w-64 p-1">
                  <NavigationMenu className="max-w-none">
                    <NavigationMenuList className="flex-col items-start gap-0">
                      {navigationLinks.map((link, index) => (
                        <NavigationMenuItem key={index} className="w-full">
                          <button
                            onClick={(e) => {
                              e.preventDefault();
                              if (router && link.href) router.push(link.href);
                            }}
                            className="flex w-full items-center rounded-md px-3 py-2 text-sm font-medium transition-colors hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground cursor-pointer no-underline"
                          >
                            {link.label}
                          </button>
                        </NavigationMenuItem>
                      ))}
                    </NavigationMenuList>
                  </NavigationMenu>
                </PopoverContent>
              </Popover>
            )}
            {/* Main nav */}
            <div className="flex items-center gap-6">
              <button
                onClick={(e) => {
                  e.preventDefault();
                  if (router) router.push("/");
                }}
                className="flex items-center space-x-2 text-primary hover:text-primary/90 transition-colors cursor-pointer"
              >
                <span className="hidden font-bold text-xl sm:inline-block">
                  BagIt
                </span>
              </button>
              {/* Navigation menu */}
              {!isMobile && (
                <NavigationMenu className="flex">
                  <NavigationMenuList className="gap-1">
                    {navigationLinks.map((link, index) => (
                      <NavigationMenuItem key={index}>
                        <NavigationMenuLink
                          href={link.href}
                          onClick={(e) => {
                            e.preventDefault();
                            if (router && link.href) router.push(link.href);
                          }}
                          className="text-muted-foreground hover:text-primary py-1.5 font-medium transition-colors cursor-pointer group inline-flex h-10 w-max items-center justify-center rounded-md bg-background px-4 py-2 text-sm focus:bg-accent focus:text-accent-foreground focus:outline-none disabled:pointer-events-none disabled:opacity-50"
                        >
                          {link.label}
                        </NavigationMenuLink>
                      </NavigationMenuItem>
                    ))}
                  </NavigationMenuList>
                </NavigationMenu>
              )}
            </div>
          </div>
          <div className="flex items-center gap-4">
            {status === AUTHJS_SESSION_STATUSES.AUTHENTICATED && data?.user ? (
              <UserMenu
                userName={data.user.name!}
                userEmail={data.user.email!}
                onItemClick={router.push}
              />
            ) : (
              <Button onClick={() => signIn("keycloak")}>
                Sign In
                <LogInIcon className="h-4 w-4" />
              </Button>
            )}
            <ModeToggle />
          </div>
        </div>
      </header>
    );
  }
);

export default Navbar;
