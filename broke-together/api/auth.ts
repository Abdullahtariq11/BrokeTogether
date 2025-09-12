import { http } from "./http";

export type AuthResult = {
  succeeded: boolean;
  accessToken: string;
  accessTokenExpiresAt: string;
  refreshToken: string;
  refreshTokenExpiresAt: string;
  user?: { id: string; email?: string; fullName?: string } | null;
  errors?: string[];
};

export async function registerApi(payload: { fullName: string; email: string; password: string }) {
  const { data } = await http.post<AuthResult>("/api/auth/register", {
    fullName: payload.fullName,
    email: payload.email,
    password: payload.password,
  });
  return data;
}

export async function loginApi(payload: { email: string; password: string }) {
  const { data } = await http.post<AuthResult>("/api/auth/login", {
    email: payload.email,
    password: payload.password,
  });
  return data;
}

export async function revokeApi() {
  // Protected endpoint; http already adds bearer token
  await http.post("/api/auth/revoke");
}

export async function userDetailsApi() {
  const { data } = await http.get<{ id: string; email: string; fullName: string }>("/api/auth/userDetails");
  return data;
}