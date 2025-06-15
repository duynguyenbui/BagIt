import Link from "next/link";
import { Menu, User } from "lucide-react";

import { Button } from "@/components/ui/button";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import { AuthActions } from "../AuthActions";
import { auth } from "@/auth";
import { ModeToggle } from "../ModeToggle";
import { Logo } from "../Logo";
import { Home, Github } from "lucide-react";

const items = [
  { name: "Home", href: "/", icon: Home, isSignedIn: false },
  {
    name: "Account",
    href: `${process.env.AUTH_KEYCLOAK_ISSUER}/account`,
    icon: User,
    isSignedIn: true,
  },
  {
    name: "Github",
    href: "https://github.com/duynguyenbui/BagIt",
    icon: Github,
    isSignedIn: false,
  },
];

export const Navbar = async () => {
  const session = await auth();
  const isSignedIn = !!session?.user;

  return (
    <nav className="sticky top-0 z-50 w-full border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
      <div className="container flex h-16 items-center justify-between px-4">
        <Logo />

        <div className="hidden md:flex items-center space-x-6">
          {items.map((item) => {
            if (item.isSignedIn && !isSignedIn) return null;
            return (
              <Link
                key={item.name}
                href={item.href}
                className="flex items-center gap-2 px-3 py-2 text-base font-medium rounded-md hover:bg-blue-50 hover:text-blue-700 transition-colors border-transparent hover:border-blue-500"
              >
                <item.icon className="h-4 w-4" />
                {item.name}
              </Link>
            );
          })}
        </div>

        <div className="flex items-center space-x-2">
          <ModeToggle />
          <AuthActions user={session?.user} />

          <Sheet>
            <SheetTrigger asChild>
              <Button variant="ghost" size="icon" className="md:hidden">
                <Menu className="h-4 w-4" />
                <span className="sr-only">Menu</span>
              </Button>
            </SheetTrigger>
            <SheetContent side="right" className="w-[300px] sm:w-[400px]">
              <SheetHeader>
                <SheetTitle className="flex items-center space-x-3">
                  <Logo />
                </SheetTitle>
                <SheetDescription>
                  Explore our products and services
                </SheetDescription>
              </SheetHeader>
              <div className="flex flex-col space-y-4 mt-4">
                <div className="flex flex-col space-y-2">
                  {items.map((item) => {
                    if (item.isSignedIn && !isSignedIn) return null;
                    return (
                      <Link
                        key={item.name}
                        href={item.href}
                        className="flex items-center gap-2 px-3 py-2 text-base font-medium rounded-md hover:bg-blue-50 hover:text-blue-700 transition-colors border-l-2 border-transparent hover:border-blue-500"
                      >
                        <item.icon className="h-4 w-4" />
                        {item.name}
                      </Link>
                    );
                  })}
                </div>
              </div>
            </SheetContent>
          </Sheet>
        </div>
      </div>
    </nav>
  );
};
