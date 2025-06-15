"use client";

import { User } from "next-auth";
import { signIn, signOut } from "next-auth/react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { ChevronDown, LogIn, LogOut, UserIcon } from "lucide-react";
import { KEYCLOAK_OAUTH_PROVIDER } from "@/constants";
import { Button } from "../ui/button";

interface AuthActionsProps {
  user?: User;
}

export const AuthActions = ({ user }: AuthActionsProps) => {
  return user ? (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="ghost" className="h-10 px-2 py-1 gap-2 rounded-full">
          <Avatar className="h-8 w-8">
            <AvatarImage src={user.image || ""} alt={user.name || ""} />
            <AvatarFallback>
              <UserIcon className="h-5 w-5" />
            </AvatarFallback>
          </Avatar>
          <span className="text-sm font-medium max-w-24 truncate">
            {user.name}
          </span>
          <ChevronDown className="h-3 w-3 opacity-50" />
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align="end" className="w-56">
        <DropdownMenuLabel className="font-normal space-y-1">
          <p className="text-sm font-medium">{user.name}</p>
          <p className="text-xs text-muted-foreground">{user.email}</p>
        </DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuItem
          onClick={() => signOut({ redirectTo: "/", redirect: true })}
          className="gap-2 text-red-600"
        >
          <LogOut className="h-4 w-4" />
          Sign Out
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  ) : (
    <Button
      variant="ghost"
      onClick={() =>
        signIn(KEYCLOAK_OAUTH_PROVIDER, { redirectTo: "/", redirect: true })
      }
      className="flex items-center space-x-2 px-3 py-2 h-auto"
    >
      <LogIn className="h-4 w-4" />
      <span>Sign In</span>
    </Button>
  );
};
