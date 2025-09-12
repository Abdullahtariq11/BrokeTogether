import * as SecureStore from "expo-secure-store";

const K_ACCESS = "bt_access_token";
const K_ACCESS_EXP = "bt_access_exp";
const K_REFRESH = "bt_refresh_token";
const K_REFRESH_EXP = "bt_refresh_exp";

let memory: {
  accessToken?: string | null;
  accessExp?: number | null;
  refreshToken?: string | null;
  refreshExp?: number | null;
} = {};

export async function saveTokens(data: {
  accessToken: string;
  accessTokenExpiresAt: string | Date;
  refreshToken: string;
  refreshTokenExpiresAt: string | Date;
}) {
  const accessExp = new Date(data.accessTokenExpiresAt).getTime();
  const refreshExp = new Date(data.refreshTokenExpiresAt).getTime();

  memory.accessToken = data.accessToken;
  memory.accessExp = accessExp;
  memory.refreshToken = data.refreshToken;
  memory.refreshExp = refreshExp;

  await SecureStore.setItemAsync(K_ACCESS, data.accessToken);
  await SecureStore.setItemAsync(K_ACCESS_EXP, String(accessExp));
  await SecureStore.setItemAsync(K_REFRESH, data.refreshToken);
  await SecureStore.setItemAsync(K_REFRESH_EXP, String(refreshExp));
}

export async function loadTokens() {
  const [a, ax, r, rx] = await Promise.all([
    SecureStore.getItemAsync(K_ACCESS),
    SecureStore.getItemAsync(K_ACCESS_EXP),
    SecureStore.getItemAsync(K_REFRESH),
    SecureStore.getItemAsync(K_REFRESH_EXP),
  ]);
  memory.accessToken = a ?? null;
  memory.accessExp = ax ? Number(ax) : null;
  memory.refreshToken = r ?? null;
  memory.refreshExp = rx ? Number(rx) : null;
  return {
    accessToken: memory.accessToken,
    accessExp: memory.accessExp,
    refreshToken: memory.refreshToken,
    refreshExp: memory.refreshExp,
  };
}

export function getAccessToken() {
  return memory.accessToken ?? null;
}

export function getRefreshToken() {
  return memory.refreshToken ?? null;
}

export async function clearTokens() {
  memory = {};
  await Promise.all([
    SecureStore.deleteItemAsync(K_ACCESS),
    SecureStore.deleteItemAsync(K_ACCESS_EXP),
    SecureStore.deleteItemAsync(K_REFRESH),
    SecureStore.deleteItemAsync(K_REFRESH_EXP),
  ]);
}

export function isAccessExpiringSoon(thresholdSeconds = 90) {
  if (!memory.accessExp) return true;
  const now = Date.now();
  return memory.accessExp - now < thresholdSeconds * 1000;
}