"use server";

import { auth } from "@/auth";
import axios from "axios";
import qs from "qs";

export const getServerToken = async () => {
  const session = await auth();
  return session?.accessToken || null;
};

export const refreshToken = async (refreshToken: string) => {
  if (!refreshToken) {
    return null;
  }

  const params = qs.stringify({
    client_id: process.env.AUTH_KEYCLOAK_ID!,
    client_secret: process.env.AUTH_KEYCLOAK_SECRET!,
    grant_type: "refresh_token",
    refresh_token: refreshToken,
  });

  const res = await axios.post(
    `${process.env.AUTH_KEYCLOAK_ISSUER}/protocol/openid-connect/token`,
    params,
    {
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
    }
  );

  if (res.status !== 200) {
    return null;
  }

  return res.data as {
    access_token: string;
    expires_in: number;
    refresh_token?: string;
    refresh_expires_in?: number;
  };
};
