import NextAuth from "next-auth";
import Keycloak from "next-auth/providers/keycloak";
import { refreshToken } from "./servers/tokens";

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    Keycloak({
      clientId: process.env.AUTH_KEYCLOAK_ID!,
      clientSecret: process.env.AUTH_KEYCLOAK_SECRET!,
      issuer: process.env.AUTH_KEYCLOAK_ISSUER!,
    }),
  ],
  callbacks: {
    async jwt({ token, account }) {
      if (account) {
        return {
          ...token,
          access_token: account.access_token,
          expires_at: account.expires_at,
          refresh_token: account.refresh_token,
          refresh_expires_at:
            Date.now() + (account.refresh_expires_in as number) * 1000,
        };
      } else if (Date.now() < (token.expires_at as any) * 1000) {
        return token;
      } else {
        const refreshedToken = await refreshToken(
          token.refresh_token as string
        );
        if (!refreshedToken) {
          return null;
        }

        return {
          ...token,
          access_token: refreshedToken.access_token,
          expires_at: Date.now() + refreshedToken.expires_in * 1000,
          refresh_token: refreshedToken.refresh_token || token.refresh_token,
          refresh_expires_at:
            Date.now() + refreshedToken.refresh_expires_in! * 1000,
        };
      }
    },
    async session({ session, token }) {
      if (token) {
        session.accessToken = token.access_token as string;
        session.refreshExpiresAt = token.refresh_expires_at as number;
      }
      return session;
    },
  },
  session: {
    strategy: "jwt",
  },
  trustHost: true,
});
