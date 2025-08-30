import axios, { AxiosError, AxiosInstance, InternalAxiosRequestConfig } from "axios";
import { API_BASE_URL } from "../config/api";
import { getAccessToken, getRefreshToken, saveTokens, clearTokens } from "../lib/tokenStorage";

type RefreshResponse = {
    succeeded: boolean;
    accessToken: string;
    accessTokenExpiresAt: string;
    refreshToken: string;
    refreshTokenExpiresAt: string;
};

let isRefreshing = false;
let queue: Array<(token?: string) => void> = [];

function onRefreshed(token?: string) {
    queue.forEach((cb) => cb(token));
    queue = [];
}

async function refreshTokens(): Promise<string | null> {
    if (isRefreshing) {
        // wait for the in-flight refresh to finish
        return new Promise((resolve) => {
            queue.push((t) => resolve(t ?? null));
        });
    }

    isRefreshing = true;
    try {
        const refreshToken = getRefreshToken();
        if (!refreshToken) throw new Error("No refresh token");

        const res = await axios.post<RefreshResponse>(`${API_BASE_URL}/api/auth/refresh`, {
            refreshToken,
        });

        if (!res.data?.succeeded) throw new Error("Refresh failed");

        await saveTokens({
            accessToken: res.data.accessToken,
            accessTokenExpiresAt: res.data.accessTokenExpiresAt,
            refreshToken: res.data.refreshToken,
            refreshTokenExpiresAt: res.data.refreshTokenExpiresAt,
        });

        const newAccess = res.data.accessToken;
        onRefreshed(newAccess);
        return newAccess;
    } catch {
        onRefreshed(undefined);
        return null;
    } finally {
        isRefreshing = false;
    }
}

export const http: AxiosInstance = axios.create({
    baseURL: API_BASE_URL,
    timeout: 20000,
});

// Attach Authorization
http.interceptors.request.use((config: InternalAxiosRequestConfig) => {
    const token = getAccessToken();
    if (token) {
        config.headers = config.headers ?? {};
        (config.headers as Record<string, string>)["Authorization"] = `Bearer ${token}`;
    }
    return config;
});

// Handle 401 → try refresh → retry once
http.interceptors.response.use(
    (res) => res,
    async (error: AxiosError) => {
        const original = error.config as InternalAxiosRequestConfig & { _retry?: boolean };

        if (error.response?.status === 401 && !original?._retry) {
            original._retry = true;

            const newToken = await refreshTokens();
            if (newToken) {
                original.headers = original.headers ?? {};
                (original.headers as Record<string, string>)["Authorization"] = `Bearer ${newToken}`;
                return http(original); // retry
            } else {
                await clearTokens();
            }
        }

        return Promise.reject(error);
    }
);